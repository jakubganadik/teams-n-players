using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace TeamsNPlayers.Infrastructure.EFCore;

public static class InfrastructureEfCoreRegistration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddDbContextFactory<ApplicationDbContext, ApplicationContextFactory>();
        services.AddScoped(provider => provider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());
        services.AddScoped(provider => provider.GetRequiredService<ApplicationDbContext>() as DbContext);
    }

    public static async Task Configure(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();

        var database = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
        
        await database.EnsureDeletedAsync();
        await database.EnsureCreatedAsync();
    }
}

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>, IDbContextFactory<ApplicationDbContext>
{
    private string ConnectionString => "Data Source=teams-n-players.db";

    public ApplicationDbContext CreateDbContext(string[] args) => CreateDbContext();
    
    public ApplicationDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        optionsBuilder.UseSqlite(ConnectionString, sqlite =>
        {
            sqlite.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}