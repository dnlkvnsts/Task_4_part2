using FluentValidation;
using Microsoft.Extensions.Localization;
using Task_4.DTOs;

namespace Task_4.Validators
{
    public class BookValidator : AbstractValidator<BookCreateDTO>
    {
        public BookValidator(IStringLocalizer<BookValidator> localizer)
        {
            RuleFor(b => b.Title)
               .NotEmpty()
               .WithMessage(localizer["TitleRequired"])
               .Length(1, 20)
               .WithMessage(localizer["TitleLength"]);

            RuleFor(b => b.PublishedYear)
                .NotEmpty()
                .WithMessage(localizer["YearRequired"])
                .InclusiveBetween(1000, DateTime.Now.Year)
                .WithMessage(localizer["YearBorder"]);

            RuleFor(b => b.AuthorId)
                .NotEmpty()
                .WithMessage(localizer["AuthorIdRequired"])
                .GreaterThan(0)
                .WithMessage(localizer["IdPositive"]);
        }
    }
}