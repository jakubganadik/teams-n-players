using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Application.Players
{
    public record DeleteSinglePlayerByIdCommand(Guid Id) : IRequest<bool>;
    internal class DeleteSinglePlayerById : IRequestHandler<DeleteSinglePlayerByIdCommand, bool>
    {
        private readonly DbContext _context;

        public DeleteSinglePlayerById(DbContext context) => _context = context;

        public async Task<bool> Handle(DeleteSinglePlayerByIdCommand request, CancellationToken cancellationToken)
        {
            var player = await _context
            .Set<Player>()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (player is null)
            {
                return false;
            }

            _context.Remove(player);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
