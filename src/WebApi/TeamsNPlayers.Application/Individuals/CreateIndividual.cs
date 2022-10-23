using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Individuals;

public record CreateIndividualCommand(Guid Id, string FirstName, string LastName) : IRequest;

internal class CreateIndividualHandler : IRequestHandler<CreateIndividualCommand>
{
    private readonly DbContext _context;

    public CreateIndividualHandler(DbContext context) => _context = context;

    public async Task<Unit> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
    {
        _context.Add(new Individual
        {
            Id = request.Id, 
            FirstName = request.FirstName, 
            LastName = request.LastName
        });

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
