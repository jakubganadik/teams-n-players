using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.WebApi.Controllers;

public record CreateTeamDto([Required] string Name);

public record UpdateTeamDto([Required] string Name);

[ApiController]
[Route("api/v1/teams")]
public class TeamsController : ControllerBase
{
    private readonly ISender _sender;
    
    public TeamsController(ISender sender) => _sender = sender; //simpler implem

    [HttpGet("")]
    public async Task<IActionResult> GetTeams() 
        => Ok(await _sender.Send(new GetAllTeamsQuery()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTeam(Guid id) 
        => Ok(await _sender.Send(new GetTeamByIdQuery(id)));

    [HttpPost("")]
    public async Task<IActionResult> CreateTeam(CreateTeamDto individual)
    {
        var id = Guid.NewGuid();

        await _sender.Send(new CreateTeamCommand(id, individual.Name));
        
        return CreatedAtAction(nameof(GetTeam), new { id }, null);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTeam(Guid id, UpdateTeamDto individual)
    {
        await _sender.Send(new UpdateTeamCommand(id, individual.Name));
        
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSingleTeamById(Guid id)
    {
        await _sender.Send(new DeleteSingleTeamByIdCommand(id));
        
        return NoContent();
    }
    
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteMultipleTeamsByIds(Guid[] ids)
    {
        await _sender.Send(new DeleteMultipleTeamsByIdsCommand(ids));
        
        return NoContent();
    }
}