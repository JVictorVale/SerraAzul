using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScottBrady91.AspNetCore.Identity;
using SerraAzul.Application.Contracts;
using SerraAzul.Application.Notifications;
using SerraAzul.Application.Services;
using SerraAzul.Core.Settings;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Domain.Entities;
using SerraAzul.Infra.Data;

namespace SerraAzul.Application;

public static class DependencyInjection
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    }
    
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        
        services.AddRepositoryDependency();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        AddServicesDependecy(services);
    }
    
    private static void AddServicesDependecy(this IServiceCollection services)
    {
        services
            .AddScoped<INotificator, Notificator>()
            .AddScoped<IPasswordHasher<Usuario>, Argon2PasswordHasher<Usuario>>();

        services
            .AddScoped<IAuthService, AuthService>() 
            .AddScoped<IUsuarioService, UsuarioService>()
            .AddScoped<IPagamentoService, PagamentoService>();
    }
}