using FluentValidation;

namespace DapperLab.Domain;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty().Length(0, 50);
    }
}