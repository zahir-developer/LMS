using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServices;
using LMS.Application.DTOs;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using LMS.Domain.Entities;
using AutoMapper;

namespace LMS.Application.Services;

public class AuthTokenService : IAuthTokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IUserServiceMapping _userService;
    private readonly IMapper _autoMapper;
    private readonly IConfiguration _config;
    public AuthTokenService(IConfiguration config, IUserServiceMapping userService, IMapper autoMapper)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtTokenKey"]));
        _userService = userService;
        _autoMapper = autoMapper;
        _config = config;
    }

    public LoginResultDto ValidateUser(LoginDto login)
    {
        var loginResultDto = new LoginResultDto();
        DateTime dateTime = DateTime.UtcNow;
        AuthTokenDto token;
        int.TryParse(_config["TokenExpireMinutes"], out int tokenExpiry);
        int.TryParse(_config["RefreshTokenExpireMinutes"], out int refreshTokenExpiry);

        if (login != null)
        {

            var user = _userService.GetUserByEmail(login.Email);

            if (user != null && user?.PasswordSalt != null)
            {
                token = new AuthTokenDto();

                HMAC hmac = new HMACSHA512(user.PasswordSalt);

                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != user.PasswordHash[i])
                        return loginResultDto;
                }

                loginResultDto = _userService.GetUserRolePrvilegeDetail(user.Id).Result;
                loginResultDto.AuthToken = token;
                loginResultDto.Password = string.Empty;
                loginResultDto.PasswordHash = null;

                token.UserId = user.Id;
                token.Token = GenerateToken(login.Email, loginResultDto.Role.RoleName);
                token.RefreshToken = GenerateRefreshToken();
                token.Email = login.Email;
                token.TokenExpiry = dateTime.AddMinutes(tokenExpiry);
                token.RefreshTokenExpiry = dateTime.AddMinutes(refreshTokenExpiry);

                return loginResultDto;
            }
        }

        return loginResultDto;
    }

    public UserDto RegisterAuthUser(AddUserDto addUser)
    {
        UserDto user = new UserDto();

        _autoMapper.Map<AddUserDto, UserDto>(addUser, user);
        var token = GenerateToken(addUser.Email);

        using var hmac = new HMACSHA512();

        user = _autoMapper.Map<AddUserDto, UserDto>(addUser);
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
        user.PasswordSalt = hmac.Key;
        user.Token = token;

        _userService.AddAsync(user);
        if (_userService.SaveChangesAsync())
            return user;
        else
            return user;
    }

    public string GenerateToken(string EmailId, string? roleName = null)
    {
        int.TryParse(_config["TokenExpireMinutes"], out int tokenExpiry);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, EmailId)
        };

        if (!string.IsNullOrEmpty(roleName))
            claims.Add(new Claim(ClaimTypes.Role, roleName));

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(tokenExpiry),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public AuthTokenDto RefreshToken(AuthTokenDto authTokenDto)
    {
        DateTime dateTime = DateTime.UtcNow;
        int.TryParse(_config["TokenExpireMinutes"], out int tokenExpiry);
        int.TryParse(_config["RefreshTokenExpireMinutes"], out int refreshTokenExpiry);
        AuthTokenDto result = new AuthTokenDto();
        if (authTokenDto is null)
        {
            return result;
        }

        var user = _userService.GetUserByEmail(authTokenDto.Email);

        if (user != null)
        {
            var loginResultDto = _userService.GetUserRolePrvilegeDetail(user.Id).Result;
            string? accessToken = authTokenDto.Token;
            string? refreshToken = authTokenDto.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return result;
            }

            result.Token = GenerateToken(loginResultDto.Email, loginResultDto.Role.RoleName);
            result.RefreshToken = GenerateRefreshToken();
            result.TokenExpiry = dateTime.AddMinutes(tokenExpiry);
            result.RefreshTokenExpiry = dateTime.AddMinutes(refreshTokenExpiry);
        }
        return result;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var key = Encoding.UTF8.GetBytes(_config["JwtTokenKey"]);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        //var tokenValidationParameters = JWTValidationParameter.GetTokenValidationParameters(jwtTokenKey);

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;

    }
}
