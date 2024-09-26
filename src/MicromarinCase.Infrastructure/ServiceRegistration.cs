using MicroMarinCase.Application.Abstractions.DynamicTools;
using MicroMarinCase.Application.Abstractions.Repositories;
using MicroMarinCase.Infrastructure.DynamicTools;
using MicroMarinCase.Infrastructure.Persistence.Context;
using MicroMarinCase.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicromarinCase.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<DynamicDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IRecordRepository, RecordRepository>();
        services.AddScoped<IJsonSenitizer, JsonSenitizer>();
    }
}
