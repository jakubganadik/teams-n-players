using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Teams;

public record DeleteMultipleTeamsByIdsCommand(Guid[] Ids) : IRequest;

internal class DeleteMultipleTeamsByIdsHandler : IRequestHandler<DeleteMultipleTeamsByIdsCommand>
{
    private readonly DbContext _context;

    public DeleteMultipleTeamsByIdsHandler(DbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteMultipleTeamsByIdsCommand request, CancellationToken cancellationToken)
    {
        var individualsToRemove = request.Ids.Select(id => new Team { Id = id });
        
        _context.Set<Team>().RemoveRange(individualsToRemove);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}