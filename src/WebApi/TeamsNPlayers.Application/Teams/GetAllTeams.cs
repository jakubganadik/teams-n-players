using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Extensions;

namespace TeamsNPlayers.Application.Teams;

public record AllTeamsResultItem(Guid Id, string Name);

public record GetAllTeamsQuery : IRequest<List<AllTeamsResultItem>>;

internal class GetAllTeamsHandler : IRequestHandler<GetAllTeamsQuery, List<AllTeamsResultItem>>
{
    private readonly DbContext _context;

    public GetAllTeamsHandler(DbContext context) => _context = context;

    public async Task<List<AllTeamsResultItem>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Query<Team>()
            .Select(i => new AllTeamsResultItem(i.Id, i.Name))
            .ToListAsync(cancellationToken);
    }
}
