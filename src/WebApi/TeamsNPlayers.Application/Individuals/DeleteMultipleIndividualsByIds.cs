using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TeamsNPlayers.Application.Individuals;

public record DeleteMultipleIndividualsByIdsCommand(Guid[] Ids) : IRequest;

internal class DeleteMultipleIndividualsByIdsHandler : IRequestHandler<DeleteMultipleIndividualsByIdsCommand>
{
    private readonly DbContext _context;

    public DeleteMultipleIndividualsByIdsHandler(DbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteMultipleIndividualsByIdsCommand request, CancellationToken cancellationToken)
    {
        var individualsToRemove = request.Ids.Select(id => new Individual { Id = id }); //how does it work
        
        _context.Set<Individual>().RemoveRange(individualsToRemove);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}