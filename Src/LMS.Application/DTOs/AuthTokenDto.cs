namespace LMS.Application.DTOs;

public class AuthTokenDto
{
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenExpiry { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
}