using ApiMiniApp.Models.Common;
using FluentValidation;

namespace ApiMiniApp.Models;

public class Organizer : AuditableEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? LogoUrl { get; set; }
    public List<Event>? Events { get; set; }
}

public class OrganizerValidator : AbstractValidator<Organizer>
{
    public OrganizerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(200);
            
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.Phone)
            .MaximumLength(20);
    }
}