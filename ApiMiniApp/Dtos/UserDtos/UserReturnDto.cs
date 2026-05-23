namespace ApiMiniApp.Dtos;

public class UserReturnDto
{
    public string UserName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
}