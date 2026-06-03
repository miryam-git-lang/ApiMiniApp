namespace WebApplication1.Dtos;

public class EventReaponseDto 
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; } 
    public string Location { get; set; } = null!;
    public string? BannerImageUrl { get; set; }
    public OrganizerInEvent? Organizer { get; set; }
}

public class OrganizerInEvent
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
}