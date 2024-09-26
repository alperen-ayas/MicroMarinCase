using FluentValidation;
using MediatR;
using MicroMarinCase.Application.Abstractions.Repositories;
using MicroMarinCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MicroMarinCase.Application.UseCases.RecordUseCases.Commands
{
    public class UpdateRecordCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public JsonObject Data { get; set; }
    }

    public class UpdateRecordCommandValidator : AbstractValidator<UpdateRecordCommand>
    {
        public UpdateRecordCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Data).NotNull().WithMessage("Data boş olamaz.");
        }
    }

    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, Result>
    {
        private readonly IRecordRepository _recordRepository;

        public UpdateRecordCommandHandler(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<Result> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var record = await _recordRepository.Get(request.Id);

            if(record==null)
                return Result.Error("Geçersiz kayıt.");

            record.UpdateData(request.Data);

            await _recordRepository.SaveChangesAsync();

            return Result.Success("Güncellendi.");
        }
    }
}
