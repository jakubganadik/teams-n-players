using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Individuals;

public record UpdateIndividualCommand(Guid Id, string FirstName, string LastName) : IRequest<bool>;

internal class UpdateIndividualHandler : IRequestHandler<UpdateIndividualCommand, bool>
{
    private readonly DbContext _context;

    public UpdateIndividualHandler(DbContext context) => _context = context;

    public async Task<bool> Handle(UpdateIndividualCommand request, CancellationToken cancellationToken)
    {
        var individual = await _context
            .Set<Individual>()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (individual is null)
        {
            return false;
        }

        individual.FirstName = request.FirstName;
        individual.LastName = request.LastName;

        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
