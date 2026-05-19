using ApiMiniApp.Models.Common;

namespace ApiMiniApp.Models;

public class Ticket : AuditableEntity
{
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
    public int EventId { get; set; }
    public Event? Event { get; set; }
}