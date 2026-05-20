using System.Net.Mail;
using ApiMiniApp.Models.Common;
using FluentValidation;
using FluentValidation.Validators;

namespace ApiMiniApp.Models;

public class Event : AuditableEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; } 
    public string Location { get; set; } = null!;
    public string? BannerImageUrl { get; set; }
    public int OrganizerId { get; set; }
    public Organizer? Organizer { get; set; }
    public List<Ticket>? Tickets { get; set; }
}

public class EventValidator : AbstractValidator<Event>
{
    public EventValidator()
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