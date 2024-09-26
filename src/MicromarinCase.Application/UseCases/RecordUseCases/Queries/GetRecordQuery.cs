using MediatR;
using MicroMarinCase.Application.Abstractions.Repositories;
using MicroMarinCase.Application.Wrappers;
using MicroMarinCase.Domain.AggregateRootModels.RecordModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroMarinCase.Application.UseCases.RecordUseCases.Queries
{
    public class GetRecordQuery : IRequest<Result<List<Record>>>
    {
        public string? Id { get; set; }
        public List<FilterParameter> Filters { get; set; } = new List<FilterParameter>();
    }
    public class FilterParameter
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string FilterType { get; set; } = "Equals"; // "Equals"
    }

    public class GetREcordQueryHandler : IRequestHandler<GetRecordQuery, Result<List<Record>>>
    {
        private readonly IRecordRepository _recordRepository;

        public GetREcordQueryHandler(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<Result<List<Record>>> Handle(GetRecordQuery request, CancellationToken cancellationToken)
        {
            var data = await _recordRepository.Get(request.Filters, request.Id, cancellationToken);

            return Result<List<Record>>.Success(data, "Succces");
        }
    }
}
