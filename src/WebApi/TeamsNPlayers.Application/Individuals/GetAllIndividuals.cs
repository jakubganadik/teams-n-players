using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamsNPlayers.Application.Extensions;

namespace TeamsNPlayers.Application.Individuals;

public record AllIndividualsResultItem(Guid Id, string FirstName, string LastName);

public record GetAllIndividualsQuery : IRequest<List<AllIndividualsResultItem>>;

internal class GetAllIndividualsHandler : IRequestHandler<GetAllIndividualsQuery, List<AllIndividualsResultItem>>
{
    private readonly DbContext _context;

    public GetAllIndividualsHandler(DbContext context) => _context = context;

    public async Task<List<AllIndividualsResultItem>> Handle(GetAllIndividualsQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Query<Individual>()
            .Select(i => new AllIndividualsResultItem(i.Id, i.FirstName, i.LastName))
            .ToListAsync(cancellationToken);
    }
}
