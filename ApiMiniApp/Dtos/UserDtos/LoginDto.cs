using FluentValidation;

namespace ApiMiniApp.Dtos;

public class LoginDto
{
    public string UserNameOrEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
}
public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserNameOrEmail)
            .NotEmpty()
            .WithMessage("Username is required");
        RuleFor(x => x.Password).
            NotEmpty()
            .MaximumLength(6)
            .WithMessage("Password is required");
    }
}