namespace ApiMiniApp.Dtos;

public class TicketReturnDto
{
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
    public int EventId { get; set; }
}