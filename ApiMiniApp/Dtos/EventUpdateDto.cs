namespace ApiMiniApp.Dtos;

public class EventUpdateDto
{    
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public int OrganizerId { get; set; }
    public IFormFile? File { get; set; }
}