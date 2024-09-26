using FluentValidation;
using MediatR;
using MicroMarinCase.Application.Abstractions.DynamicTools;
using MicroMarinCase.Application.Abstractions.Repositories;
using MicroMarinCase.Application.Wrappers;
using System.Text.Json.Nodes;

namespace MicroMarinCase.Application.UseCases.RecordUseCases.Commands
{
    public class CreateRecordCommand : IRequest<Result>
    {
        public string RecordType { get; set; }
        public JsonObject Data { get; set; }
    }
    public class CreateRecordCommandValidator : AbstractValidator<CreateRecordCommand>
    {
        public CreateRecordCommandValidator()
        {
            When(x => x.RecordType == "customer", () =>
            {
                RuleFor(x => x.Data["adress"])
                .NotNull()
                .WithMessage("Items alanı olmak zorundadır.")
                .Must(items => items is JsonArray array && array.Count > 0)
                .WithMessage("Items dizisi en az bir eleman içermelidir.");

            });

            
            When(x => x.RecordType == "order", () =>
            {
                RuleFor(x => x.Data["items"])
                .NotNull()
                .WithMessage("Items alanı olmak zorundadır.")
                .Must(items => items is JsonArray array && array.Count > 0)
                .WithMessage("Items dizisi en az bir eleman içermelidir.");

                RuleFor(x => x.Data["customerId"])
                .NotEmpty()
                .WithMessage("CustomerId alanı boş olmamalıdır.");
            });
        }
    }
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommand, Result>
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IJsonSenitizer _jsonSenitizer;

        public CreateRecordCommandHandler(IRecordRepository recordRepository, IJsonSenitizer jsonSenitizer)
        {
            _recordRepository = recordRepository;
            _jsonSenitizer = jsonSenitizer;
        }

        public async Task<Result> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
        {
            var records = _jsonSenitizer.CollectRecords(request.Data, request.RecordType, null, true);

            await _recordRepository.CreateRange(records);

            await _recordRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Success");
        }
    }
}
