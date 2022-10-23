using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Extensions;

public static class DbContextQueryExtensions
{
    public static IQueryable<TEntity> Query<TEntity>(this DbContext context) where TEntity : class
        => context.Set<TEntity>().AsNoTracking();
}