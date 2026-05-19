using ApiMiniApp.Models;

namespace ApiMiniApp.Dtos;

public class EventReturnDto 
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; } 
    public string Location { get; set; } = null!;
    public string? BannerImageUrl { get; set; }
    public List<OrganizerInEventReturnDto>? Organizers { get; set; }
}

public class OrganizerInEventReturnDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
}
