using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Extensions;

namespace TeamsNPlayers.Application.Teams;

public record TeamByIdResult(Guid Id, string Name);

public record GetTeamByIdQuery(Guid Id) : IRequest<TeamByIdResult>;

internal class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamByIdResult?>
{
    private readonly DbContext _context;

    public GetTeamByIdHandler(DbContext context) => _context = context;

    public async Task<TeamByIdResult?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Query<Team>()
            .Where(i => i.Id == request.Id)
            .Select(i => new TeamByIdResult(i.Id, i.Name))
            .FirstOrDefaultAsync(cancellationToken);;
    }
}
