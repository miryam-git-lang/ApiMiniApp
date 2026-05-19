using ApiMiniApp.Models.Common;

namespace ApiMiniApp.Models;

public class Organizer : AuditableEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? LogoUrl { get; set; }
    public List<Event>? Events { get; set; }
}