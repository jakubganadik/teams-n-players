using Microsoft.AspNetCore.Mvc;

namespace TeamsNPlayers.WebApi.Controllers;

[ApiController]
[Route("api/v1/players")]
public class PlayersController : ControllerBase
{
    [HttpGet("")]
    public IActionResult GetPlayers()
    {
        return Ok(new [] 
        {
            new
            {
                Id = new Guid("35c4b7bc-ef5f-43cb-b902-b8d3d9d87d90"),
                TeamId = new Guid("79d1705c-4864-4a29-a864-2ec582bb6342"),
                IndividualId = new Guid("66a4657a-cdcc-4df5-baa1-1a9c81ba5044"),
                TeamName = "FC Foo",
                PlayerName = "DOE John",
                ShirtNumber = 77,
                Position = "GLK"
            }
        });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetPlayer(Guid id)
    {
        return Ok(new
        {
            Id = id,
            TeamId = new Guid("79d1705c-4864-4a29-a864-2ec582bb6342"),
            IndividualId = new Guid("66a4657a-cdcc-4df5-baa1-1a9c81ba5044"),
            TeamName = "FC Foo",
            PlayerName = "DOE John",
            ShirtNumber = 77,
            Position = "GLK"
        });
    }

    [HttpPost("")]
    public IActionResult CreatePlayer() 
        => CreatedAtAction(nameof(GetPlayer), new { id = Guid.NewGuid() }, null);
    
    [HttpPut("{id:guid}")]
    public IActionResult UpdatePlayer() 
        => NoContent();

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteSinglePlayerById() 
        => NoContent();

    [HttpPost("delete")]
    public Task<IActionResult> DeleteMultiplePlayersByIds(Guid[] ids) 
        => Task.FromResult<IActionResult>(NoContent());
}
