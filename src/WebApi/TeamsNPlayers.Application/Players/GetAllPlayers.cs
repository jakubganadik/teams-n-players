using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Extensions;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Application.Players
{

    public record PlayerItem(Guid Id, Guid TeamId, Guid IndividualId, string TeamName, string PlayerName, ushort ShirtNumber, string Position);
    public record GetAllPlayersQuery : IRequest<List<PlayerItem>>;
    internal class GetAllPlayers : IRequestHandler<GetAllPlayersQuery, List<PlayerItem>>
    {
        private readonly DbContext _context;

        public GetAllPlayers(DbContext context) => _context = context;

        public async Task<List<PlayerItem>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            return await _context
                .Query<Player>()
                .Select(p => new PlayerItem(p.Id, p.TeamId, p.IndividualId, p.Team.Name, p.Individual.FirstName + " " + p.Individual.LastName, p.ShirtNumber, p.Position))
                .ToListAsync(cancellationToken);
        }
    }
}
