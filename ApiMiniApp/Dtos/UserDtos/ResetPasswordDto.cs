namespace ApiMiniApp.Dtos;

public class ResetPasswordDto
{
    public string Email { get; set; } = null!;
    public string Token { get; set; }
    public string NewPassword { get; set; } = null!;
    
}