namespace WebApplication1.Dtos;

public class TokenResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}