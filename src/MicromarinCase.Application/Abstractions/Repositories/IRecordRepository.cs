using Beymen.ECommerce.Common.Domain.SeedWorks;
using MicroMarinCase.Application.UseCases.RecordUseCases.Queries;
using MicroMarinCase.Domain.AggregateRootModels.RecordModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCase.Application.Abstractions.Repositories;

public interface IRecordRepository : IUnitOfWork
{
    Task Create(Record record, CancellationToken cancellationToken = default);
    Task CreateRange(List<Record> records, CancellationToken cancellationToken = default);
    Task Update(Record record);
    Task Delete(Record record);
    Task<List<Record>> Get(List<FilterParameter> filters, string id = null, CancellationToken cancellationToken = default);
    Task<Record> Get(string id = null, CancellationToken cancellationToken = default);
}
