using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Infrustructure.Persistence;
using OnlineJobMarketplace.Infrustructure.Persistence.Interceptors;
using OnlineJobMarketplace.Infrustructure.Services;

namespace OnlineJobMarketplace.Infrustructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString: configuration.GetConnectionString("DbConnection"));
        });

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        
        services.AddTransient<IGuidGenerator, GuidGeneratorService>();
        
        return services;
    }
}