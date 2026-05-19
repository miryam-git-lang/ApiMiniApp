namespace ApiMiniApp.Dtos;

public class TicketCreateDto
{
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
    public int EventId { get; set; }
    public IFormFile? File { get; set; }
}