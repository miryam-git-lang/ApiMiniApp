using ApiMiniApp.Models;
using FluentValidation;

namespace ApiMiniApp.Dtos;

public class TicketCreateDto
{
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
    public int EventId { get; set; }
}
public class TicketCreateDtoValidator : AbstractValidator<TicketCreateDto>
{
    public TicketCreateDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type is required")
            .MaximumLength(20);
        
        RuleFor(x => x.Price)
            .Must(price => price >= 0)
            .WithMessage("Price must be a non-negative value");
        
        RuleFor(x => x.QuantityAvailable)  
            .NotEmpty()
            .Must(quantityAvailable => quantityAvailable > 0)
            .WithMessage("Quantity available must be greater than zero");
                
    }
}