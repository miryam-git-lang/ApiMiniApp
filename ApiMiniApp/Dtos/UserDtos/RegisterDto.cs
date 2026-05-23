using ApiMiniApp.Models;
using FluentValidation;

namespace ApiMiniApp.Dtos;

public class RegisterDto
{
    public string FullName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string RePassword { get; set; }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDto> 
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name cannot exceed 50 characters");
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MaximumLength(50)
            .WithMessage("Username cannot exceed 50 characters");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .MaximumLength(50)
            .WithMessage("Email cannot exceed 50 characters");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.RePassword)
            .NotEmpty()
            .WithMessage("RePassword is required")
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");
    }
    
}