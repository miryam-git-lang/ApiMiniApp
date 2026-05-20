namespace ApiMiniApp.Dtos;

public class TicketUpdateDto
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
}