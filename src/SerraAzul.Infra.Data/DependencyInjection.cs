using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Infra.Data.Context;
using SerraAzul.Infra.Data.Repositories;

namespace SerraAzul.Infra.Data;

public static class DependencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddDbContext<SerraAzulDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }
    
    public static void AddRepositoryDependency(this IServiceCollection service)
    {
        service.AddScoped<IUsuarioRepository, UsuarioRepository>();
        service.AddScoped<IPagamentoRepository, PagamentoRepository>();
    }
}