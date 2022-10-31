using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using TeamsNPlayers.Application.Individuals;
using TeamsNPlayers.Application.Players;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.WebApi.Controllers;

public record CreatePlayerDto([Required] Guid TeamId, [Required] Guid IndividualId, [Required] ushort ShirtNumber, [Required] string Position);
public record UpdatePlayerDto([Required] Guid id, [Required] Guid TeamId, [Required] Guid IndividualId, [Required]  ushort ShirtNumber,[Required] string Position);

[ApiController]
[Route("api/v1/players")]
public class PlayersController : ControllerBase
{

    private readonly ISender _sender;

    public PlayersController(ISender sender) => _sender = sender; //simpler implem

    [HttpGet("")]
    public async Task<IActionResult> GetPlayers()
    => Ok(await _sender.Send(new GetAllPlayersQuery()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPlayer(Guid id)
        => Ok(await _sender.Send(new GetPlayerByIdQuery(id)));

    [HttpPost("")]
    public async Task<IActionResult> CreatePlayer(CreatePlayerDto player)
    {

        var id = Guid.NewGuid();

        await _sender.Send(new CreatePlayerCommand(id, player.TeamId, player.ShirtNumber, player.IndividualId, player.Position));
        return CreatedAtAction(nameof(GetPlayer), new { id }, null); //???
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePlayer(UpdatePlayerDto player)
    {
        await _sender.Send(new UpdatePlayerCommand(player.id, player.TeamId, player.IndividualId, player.ShirtNumber, player.Position));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSinglePlayerById(Guid id)
    {
        await _sender.Send(new DeleteSinglePlayerByIdCommand(id));
        return NoContent();
    }
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteMultiplePlayersByIds(Guid[] ids)
    {
        await _sender.Send(new DeleteMultiplePlayersByIdCommand(ids));
        return NoContent();
    }
        
}
