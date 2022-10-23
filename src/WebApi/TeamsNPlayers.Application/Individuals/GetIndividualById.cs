using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Extensions;

namespace TeamsNPlayers.Application.Individuals;

public record IndividualByIdResult(Guid Id, string FirstName, string LastName);

public record GetIndividualByIdQuery(Guid Id) : IRequest<IndividualByIdResult>;

internal class GetIndividualByIdHandler : IRequestHandler<GetIndividualByIdQuery, IndividualByIdResult?>
{
    private readonly DbContext _context;

    public GetIndividualByIdHandler(DbContext context) => _context = context;

    public async Task<IndividualByIdResult?> Handle(GetIndividualByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Query<Individual>()
            .Where(i => i.Id == request.Id)
            .Select(i => new IndividualByIdResult(i.Id, i.FirstName, i.LastName))
            .FirstOrDefaultAsync(cancellationToken);;
    }
}
