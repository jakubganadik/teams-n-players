using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Teams;

public record UpdateTeamCommand(Guid Id, string Name) : IRequest<bool>;

internal class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, bool>
{
    private readonly DbContext _context;

    public UpdateTeamHandler(DbContext context) => _context = context;

    public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var individual = await _context
            .Set<Team>()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (individual is null)
        {
            return false;
        }

        individual.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
