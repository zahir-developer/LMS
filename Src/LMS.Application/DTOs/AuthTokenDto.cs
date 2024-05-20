namespace LMS.Application.DTOs;

public class AuthTokenDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}