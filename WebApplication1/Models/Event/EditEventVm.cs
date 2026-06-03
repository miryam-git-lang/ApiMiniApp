namespace WebApplication1.Models;

public class EditEventVm
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public int OrganizerId { get; set; }
}