using FluentValidation;
using RedditMockup.Model.Entities;

namespace RedditMockup.Common.Validations;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);

        RuleFor(x => x.Family).NotEmpty().MaximumLength(20);
    }
}