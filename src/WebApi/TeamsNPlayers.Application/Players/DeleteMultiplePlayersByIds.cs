using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Application.Players
{
    public record DeleteMultiplePlayersByIdCommand(Guid[] Ids) : IRequest;
    internal class DeleteMultiplePlayersByIds : IRequestHandler<DeleteMultiplePlayersByIdCommand>
    {
        private readonly DbContext _context;

        public DeleteMultiplePlayersByIds(DbContext context) => _context = context;
        public async Task<Unit> Handle(DeleteMultiplePlayersByIdCommand request, CancellationToken cancellationToken)
        {
            var individualsToRemove = request.Ids.Select(id => new Player { Id = id });//delete according to pk, the Team does not have to be complete

            _context.Set<Player>().RemoveRange(individualsToRemove);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
