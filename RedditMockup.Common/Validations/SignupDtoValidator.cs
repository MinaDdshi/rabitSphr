using FluentValidation;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Common.Validations;

public class SignupDtoValidator : AbstractValidator<UserDto>
{

    public SignupDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(20).MinimumLength(8);

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);

        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);

        RuleFor(x => x.Family).NotEmpty().MaximumLength(20);
    }
}