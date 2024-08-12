using GameStoreRemake.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStoreRemake.Data;

public static class DataExtensions
{
    public static async Task InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServer<ApplicationDbContext>(connectionString)
            .AddScoped<IGamesRepositories, EntityFrameworkGamesRepository>(); ;

        return services;
    }
}
