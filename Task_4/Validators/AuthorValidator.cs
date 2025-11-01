using FluentValidation;
using Task_4.DTOs;
using Microsoft.Extensions.Localization;

namespace Task_4.Validators
{

    public class AuthorValidator : AbstractValidator<AuthorCreateDTO>
    {
        public AuthorValidator(IStringLocalizer<AuthorValidator> localizer)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage(localizer["NameRequired"]) 

                .Matches(@"^[a-zA-Zа-яА-Я\s'-]+$")
                .WithMessage(localizer["NamePattern"]) 

                .Length(1, 20)
                .WithMessage(localizer["NameLength"]); 

            RuleFor(a => a.DateOfBirth)
                .NotEmpty()
                .WithMessage(localizer["BirthDateRequired"]) 
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage(localizer["BirthDateInFuture"]) 
                .When(x => x.DateOfBirth.HasValue);
        }
    }
}