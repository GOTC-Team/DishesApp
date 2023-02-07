using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;
using System.Reflection;

namespace Notes.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString
                ,opt => 
                    opt.MigrationsAssembly(Assembly
                                            .GetExecutingAssembly()
                                            .FullName) 
                );
        });
        services.AddScoped<IApplicationContext>(provider => 
                provider.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}
