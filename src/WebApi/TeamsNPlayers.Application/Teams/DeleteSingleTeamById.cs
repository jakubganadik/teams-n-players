using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Teams;

public record DeleteSingleTeamByIdCommand(Guid Id) : IRequest<bool>;

internal class DeleteSingleTeamByIdHandler : IRequestHandler<DeleteSingleTeamByIdCommand, bool>
{
    private readonly DbContext _context;

    public DeleteSingleTeamByIdHandler(DbContext context) => _context = context;

    public async Task<bool> Handle(DeleteSingleTeamByIdCommand request, CancellationToken cancellationToken)
    {
        var individual = await _context
            .Set<Team>()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (individual is null)
        {
            return false;
        }
        
        _context.Remove(individual);

        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
