using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamsNPlayers.Application.Individuals;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Application.Players
{
    public record CreatePlayerCommand(Guid Id, Guid TeamId, ushort ShirtNumber, Guid IndividualId, string Position) : IRequest; //(Guid Id, Guid TeamId, ushort ShirtNumber, Guid IndividualId, string Position) : IRequest
    internal class CreatePlayer : IRequestHandler<CreatePlayerCommand>
    {
        private readonly DbContext _context;

        public CreatePlayer(DbContext context) => _context = context;
        public async Task<Unit> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {

            _context.Add(new Player
            {
                Id = request.Id,
                TeamId = request.TeamId,
                ShirtNumber = request.ShirtNumber,
                IndividualId = request.IndividualId,
                Position = request.Position

            });

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
