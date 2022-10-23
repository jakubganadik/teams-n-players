using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Individuals;

public record DeleteSingleIndividualByIdCommand(Guid Id) : IRequest<bool>;

internal class DeleteSingleIndividualByIdHandler : IRequestHandler<DeleteSingleIndividualByIdCommand, bool>
{
    private readonly DbContext _context;

    public DeleteSingleIndividualByIdHandler(DbContext context) => _context = context;

    public async Task<bool> Handle(DeleteSingleIndividualByIdCommand request, CancellationToken cancellationToken)
    {
        var individual = await _context
            .Set<Individual>()
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
