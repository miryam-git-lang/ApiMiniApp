using ApiMiniApp.Models;
using FluentValidation;

namespace ApiMiniApp.Dtos;

public class EventCreateDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; } 
    public string Location { get; set; } = null!;
    public int OrganizerId { get; set; }
}
public class EventCreateDtoValidator : AbstractValidator<EventCreateDto>
{
    public EventCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(200);
        RuleFor(x => x.Description)
            .MaximumLength(200);
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required")
            .Must(date => date > DateTime.Now)
            .WithMessage("Event date must be in the future");
        RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("Location is required")
            .MaximumLength(200);

    }
}