using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Task_4.DTOs;

namespace Task_4.Filters
{
    public class ValidFilter : IAsyncActionFilter
    {
        private readonly IValidator<AuthorCreateDTO> _authorValidator;
        private readonly IValidator<BookCreateDTO> _bookValidator;

        public ValidFilter(
            IValidator<AuthorCreateDTO> authorValidator,
            IValidator<BookCreateDTO> bookValidator)
        {
            _authorValidator = authorValidator;
            _bookValidator = bookValidator;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("dto", out var dto))
            {
                if (dto is AuthorCreateDTO authorDto)
                {
                    var result = await _authorValidator.ValidateAsync(authorDto);
                    if (!result.IsValid)
                    {
                        var errors = result.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToArray()
                            );

                        context.Result = new BadRequestObjectResult(new
                        {
                            message = "Validation failed",
                            errors = errors
                        });
                        return;
                    }
                }

                if (dto is BookCreateDTO bookDto)
                {
                    var result = await _bookValidator.ValidateAsync(bookDto);
                    if (!result.IsValid)
                    {
                        var errors = result.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToArray()
                            );

                        context.Result = new BadRequestObjectResult(new
                        {
                            message = "Validation failed",
                            errors = errors
                        });
                        return;
                    }
                }
            }

            await next();
        }
    }
}