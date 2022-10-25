using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Individuals;
using TeamsNPlayers.Application.Teams;
using TeamsNPlayers.Application.Players;

namespace TeamsNPlayers.Infrastructure.EFCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Individual>? Individuals { get; set; }
    public DbSet<Team>? Teams { get; set; }

    public DbSet<Player>? Players { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}