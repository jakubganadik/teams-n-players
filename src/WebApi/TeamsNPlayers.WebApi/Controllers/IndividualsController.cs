using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamsNPlayers.Application.Individuals;

namespace TeamsNPlayers.WebApi.Controllers;

public record CreateIndividualDto([Required] string FirstName, [Required] string LastName);

public record UpdateIndividualDto([Required] string FirstName, [Required] string LastName);

[ApiController]
[Route("api/v1/individuals")]
public class IndividualsController : ControllerBase
{
    private readonly ISender _sender;

    public IndividualsController(ISender sender) => _sender = sender;

    [HttpGet("")]
    public async Task<IActionResult> GetIndividuals() 
        => Ok(await _sender.Send(new GetAllIndividualsQuery()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetIndividual(Guid id) 
        => Ok(await _sender.Send(new GetIndividualByIdQuery(id)));

    [HttpPost("")]
    public async Task<IActionResult> CreateIndividual(CreateIndividualDto individual)
    {
        var id = Guid.NewGuid();

        await _sender.Send(new CreateIndividualCommand(id, individual.FirstName, individual.LastName));
        
        return CreatedAtAction(nameof(GetIndividual), new { id }, null);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateIndividual(Guid id, UpdateIndividualDto individual)
    {
        await _sender.Send(new UpdateIndividualCommand(id, individual.FirstName, individual.LastName));
        
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSingleIndividualById(Guid id)
    {
        await _sender.Send(new DeleteSingleIndividualByIdCommand(id));
        
        return NoContent();
    }
    
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteMultipleIndividualsByIds(Guid[] ids)
    {
        await _sender.Send(new DeleteMultipleIndividualsByIdsCommand(ids));
        
        return NoContent();
    }
}
