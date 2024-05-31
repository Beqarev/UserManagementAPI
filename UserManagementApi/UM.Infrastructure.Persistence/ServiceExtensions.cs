using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UM.Core.Application.Interfaces;
using UM.Infrastructure.Persistence.Data;
using UM.Infrastructure.Persistence.Implementations;

namespace UM.Infrastructure.Persistence;

public static class ServiceExtensions
{
    public static void AddPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}