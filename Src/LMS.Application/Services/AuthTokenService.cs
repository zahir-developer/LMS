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

using LMS.Application.Interfaces.IServices;
using LMS.Application.DTOs;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using AutoMapper;

namespace LMS.Application.Services;

public class AuthTokenService : IAuthTokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IUserService _userService;

    private readonly IMapper _autoMapper;

    public AuthTokenService(IConfiguration config, IUserService userService, IMapper autoMapper)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtTokenKey"]));
        _userService = userService;
        _autoMapper = autoMapper;
    }

    public AuthTokenDto ValidateUser(LoginDto login)
    {
        AuthTokenDto token = null;

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
                        return null;
                }

                token.Token = GenerateToken(login.Email);
                token.RefreshToken = string.Empty;
                token.Email = login.Email;

                return token;
            }            
        }

        return token;
    }

    public UserDto RegisterAuthUser(AddUserDto addUser)
    {
        UserDto user = _autoMapper.Map<UserDto>(addUser);
        var token = GenerateToken(addUser.Email);

        using var hmac = new HMACSHA512();

        user = _autoMapper.Map<AddUserDto, UserDto>(addUser);
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
        user.PasswordSalt = hmac.Key;
        user.Token = token;
        

        _userService.AddAsync(user);

        return user;
    }

    public string GenerateToken(string EmailId)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, EmailId)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
