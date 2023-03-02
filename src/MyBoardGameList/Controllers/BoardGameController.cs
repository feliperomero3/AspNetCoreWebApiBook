using Microsoft.AspNetCore.Mvc;

namespace MyBoardGameList.Controllers;

[Route("boardgame")]
[ApiController]
public class BoardGameController : ControllerBase
{
    private readonly ILogger<BoardGameController> _logger;

    public BoardGameController(ILogger<BoardGameController> logger)
    {
        _logger = logger;
    }

    public IEnumerable<BoardGame> GetBoardGames()
    {
        return new[]
        {
            new BoardGame()
            {
                Id = 1,
                Name = "Axis & Allies",
                Year = 1981
            },
            new BoardGame()
            {
                Id = 2,
                Name = "Citadels",
                Year = 2000
            },
            new BoardGame()
            {
                Id = 3,

                Name = "Terra-forming Mars",
                Year = 2016
            }
        };
    }
}
