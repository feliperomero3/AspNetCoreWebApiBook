using Microsoft.AspNetCore.Mvc;
using MyBoardGameList.Entities;
using MyBoardGameList.Models;

namespace MyBoardGameList.Controllers;

[Route("boardgames")]
[ApiController]
public class BoardGamesController : ControllerBase
{
    private readonly ILogger<BoardGamesController> _logger;

    public BoardGamesController(ILogger<BoardGamesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetBoardGames")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    public RestModel<BoardGame[]> GetBoardGames()
    {
        var games = new[]
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

        return new RestModel<BoardGame[]>
        {
            Data = games,
            Links = new[]
            {
                new LinkModel
                {
                    Href = Url.Action("GetBoardGames", "BoardGames", null, Request.Scheme)!,
                    Rel = "Self",
                    Type = HttpMethod.Get.Method
                }
            }
        };
    }
}
