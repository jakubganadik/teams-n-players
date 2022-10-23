using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Teams;

public record CreateTeamCommand(Guid Id, string Name) : IRequest;

internal class CreateIndividualHandler : IRequestHandler<CreateTeamCommand>
{
    private readonly DbContext _context;

    public CreateIndividualHandler(DbContext context) => _context = context;

    public async Task<Unit> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        _context.Add(new Team
        {
            Id = request.Id, 
            Name = request.Name
        });

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
