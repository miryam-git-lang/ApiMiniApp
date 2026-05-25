namespace ApiMiniApp.Models;

public class RefreshTokenSetting
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string UserId { get; set; }
    public AppUser? User { get; set; }
}