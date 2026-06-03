namespace WebApplication1.Models;

public class CreateEventFileVm
{
    public int EventId { get; set; }
    public IFormFile? File { get; set; }
}