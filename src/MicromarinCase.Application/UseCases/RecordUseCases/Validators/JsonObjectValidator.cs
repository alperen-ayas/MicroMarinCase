using FluentValidation;
using System.Text.Json.Nodes;

namespace MicroMarinCase.Application.UseCases.RecordUseCases.Validators
{
    public class JsonObjectValidator : AbstractValidator<JsonObject>
    {
        public JsonObjectValidator()
        {
            RuleFor(jsonObject => jsonObject["items"])
            .NotNull()
            .WithMessage("Items alanı olmak zorundadır.")
            .Must(items => items is JsonArray array && array.Count > 0)
            .WithMessage("Items dizisi en az bir eleman içermelidir.");

            RuleFor(jsonObject => jsonObject["customerId"])
            .NotEmpty()
            .WithMessage("CustomerId alanı boş olmamalıdır.");
        }
    }
}
