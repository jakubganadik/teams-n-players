using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Extensions;

namespace TeamsNPlayers.Application.Players
{
    public record PlayerItemResult(Guid Id, Guid TeamId, Guid IndividualId, string TeamName, string PlayerName, ushort ShirtNumber, string Position);
    public record GetPlayerByIdQuery(Guid Id): IRequest<PlayerItemResult>;
    internal class GetPlayerById : IRequestHandler<GetPlayerByIdQuery, PlayerItemResult?>
    {
        private readonly DbContext _context;

        public GetPlayerById(DbContext context) => _context = context;
        public async Task<PlayerItemResult?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context
                .Query<Player>()
                .Where(p => p.Id == request.Id)
                .Select(p => new PlayerItemResult(p.Id, p.TeamId, p.IndividualId, p.Team.Name, p.Individual.FirstName + " " + p.Individual.LastName, p.ShirtNumber, p.Position))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
