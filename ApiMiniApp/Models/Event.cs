using ApiMiniApp.Models.Common;

namespace ApiMiniApp.Models;

public class Event : AuditableEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; } 
    public string Location { get; set; } = null!;
    public string? BannerImageUrl { get; set; }
    public int OrganizerId { get; set; }
    public Organizer? Organizer { get; set; }
    public List<Ticket>? Tickets { get; set; }
}