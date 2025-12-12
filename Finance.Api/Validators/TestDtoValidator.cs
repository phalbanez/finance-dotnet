using Finance.Shared.Dtos;
using FluentValidation;

namespace Finance.Api.Validators;

public class TestDtoValidator : AbstractValidator<TestDto>
{
    public TestDtoValidator()
    {
        RuleFor(test => test.Name).NotEmpty().Length(3, 100);

        RuleFor(test => test.Email).NotEmpty().Length(3, 100).EmailAddress();

        RuleFor(test => test.Value).GreaterThan(0);
    }

}
