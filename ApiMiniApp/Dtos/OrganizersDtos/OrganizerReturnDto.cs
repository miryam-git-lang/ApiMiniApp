namespace ApiMiniApp.Dtos;

public class OrganizerReturnDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public List<EventsInOrganizerReturnDto>? Events { get; set; }
}
public class EventsInOrganizerReturnDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; } 
    public string Location { get; set; } = null!;
}