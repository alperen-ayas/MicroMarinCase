using MicroMarinCase.Application.Abstractions.Repositories;
using MicroMarinCase.Application.Exceptions;
using MicroMarinCase.Application.UseCases.RecordUseCases.Queries;
using MicroMarinCase.Domain.AggregateRootModels.RecordModels;
using MicroMarinCase.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroMarinCase.Infrastructure.Persistence.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly DynamicDbContext _dbContext;

        public RecordRepository(DynamicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Record record)
        {
            await _dbContext.Set<Record>().AddAsync(record);
        }

        public async Task CreateRange(List<Record> records)
        {
            await _dbContext.Set<Record>().AddRangeAsync(records);
        }

        public async Task Delete(Record record)
        {
            _dbContext.Set<Record>().Remove(record);
        }

        public async Task<List<Record>> Get(List<FilterParameter> filters, string id = null,CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Record>().AsQueryable();

            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(record => record.Id == id);
            }

            foreach (var filter in filters)
            {
                if (filter.FilterType == "Equals")
                    query = query.Where(record => DbFunctionExtensions.JsonValue(record.Data, $"$.{filter.Key}") == filter.Value);
                //else if (filter.FilterType == "GreaterThan")
                //{
                //    int intVal;
                //    DateTime dateVal;
                //    if (int.TryParse(filter.Value, out var filterValue))
                //        query = query.Where(record => EF.Functions DbFunctionExtensions.JsonValue(record.Data, $"$.{filter.Key}") >= filterValue);
                //    else if (DateTime.TryParse(filter.Value, out var filterDatetime))
                //        query = query.Where(record => DateTime.Parse(DbFunctionExtensions.JsonValue(record.Data, $"$.{filter.Key}")) >= filterDatetime);
                //    else
                //        throw new InvalidFilterException("Filtre değeri bir sayı veya tarihe çevrilemiyor.");
                //}
                //else if (filter.FilterType == "LessThan")
                //{
                //    int intVal;
                //    DateTime dateVal;
                //    if (int.TryParse(filter.Value, out var filterValue))
                //        query = query.Where(record => int.Parse(DbFunctionExtensions.JsonValue(record.Data, $"$.{filter.Key}")) <= int.Parse(filter.Value));
                //    else if(DateTime.TryParse(filter.Value,out var filterDatetime))
                //        query = query.Where(record => DateTime.Parse(DbFunctionExtensions.JsonValue(record.Data, $"$.{filter.Key}")) <= DateTime.Parse(filter.Value));
                //    else
                //        throw new InvalidFilterException("Filtre değeri bir sayı veya tarihe çevrilemiyor.");
                //}
                //else if(filter.FilterType == "Include" && filter.Key != null)
                //{

                //}
                else
                {
                    throw new NotImplementedException($"Bu filtre tipi({filter.FilterType}) filtre çeşitlerimize dahil değildir.");
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Record> Get(string id = null, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Record>().FindAsync(id, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Record record)
        {
            await Task.Run(()=> _dbContext.Set<Record>().Update(record));
        }
    }
}
