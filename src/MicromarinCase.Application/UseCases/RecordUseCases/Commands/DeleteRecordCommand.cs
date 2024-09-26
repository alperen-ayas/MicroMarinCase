using FluentValidation;
using MediatR;
using MicroMarinCase.Application.Abstractions.Repositories;
using MicroMarinCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCase.Application.UseCases.RecordUseCases.Commands
{
    public class DeleteRecordCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }

    public class DeleteRecordCommandValdiator : AbstractValidator<DeleteRecordCommand>
    {
        public DeleteRecordCommandValdiator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id değeri boş olamaz.");
        }
    }

    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand, Result>
    {
        private readonly IRecordRepository _recordRepository;

        public DeleteRecordCommandHandler(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<Result> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            var record = await _recordRepository.Get(request.Id, cancellationToken);

            await _recordRepository.Delete(record);

            await _recordRepository.SaveChangesAsync(cancellationToken);

            if (record == null)
                return Result.Error("Operation failed");

            return Result.Success("Succefully deleted.");
        }
    }
}
