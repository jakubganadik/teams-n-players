using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsNPlayers.Application.Extensions;
using TeamsNPlayers.Application.Individuals;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Application.Players
{
    public record UpdatePlayerCommand(Guid Id, Guid TeamId, Guid IndividualId, ushort ShirtNumber, string Position) : IRequest<bool>;
    internal class UpdatePlayer : IRequestHandler<UpdatePlayerCommand,bool>
    {
        DbContext _context;
        
        public UpdatePlayer(DbContext context) => _context = context;
        public async Task<bool> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _context
            .Set<Player>()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (player is null)
            {
                return false;
            }
            player.TeamId = request.TeamId;
            player.IndividualId = request.IndividualId;
            player.ShirtNumber = request.ShirtNumber;
            player.Position = request.Position;
            /*
            var team = await _context.Query<Team>().Where(t => t.Name == request.TeamName).FirstOrDefaultAsync(cancellationToken);//maybe not neccesary
            
            if (team is not null)
            {
                player.TeamId = team.Id;
            }
            */


            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
