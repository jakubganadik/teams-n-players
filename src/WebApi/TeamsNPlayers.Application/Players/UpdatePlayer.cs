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
    public record UpdatePlayerCommand(Guid Id, string? TeamName, ushort? ShirtNumber, string? Position) : IRequest<bool>;
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
            
            var teamid = await _context.Query<Team>().Where(t => t.Name == request.TeamName).Select(t => t.Id).FirstOrDefaultAsync(cancellationToken);//maybe not neccesary
            if (teamid == null)//why
            {
                player.TeamId = teamid;
            }
            
            
                
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
