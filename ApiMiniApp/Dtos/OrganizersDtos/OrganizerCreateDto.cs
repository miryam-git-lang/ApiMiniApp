using ApiMiniApp.Models;
using FluentValidation;

namespace ApiMiniApp.Dtos;

public class OrganizerCreateDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    
}
public class OrganizerCreateDtoValidator : AbstractValidator<OrganizerCreateDto>
{
    public OrganizerCreateDtoValidator()
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