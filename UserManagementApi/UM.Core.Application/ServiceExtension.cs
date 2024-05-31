using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UM.Core.Application.Interfaces;
using UM.Core.Application.Services;

namespace UM.Core.Application;

public static class ServiceExtension
{
    public static void AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}