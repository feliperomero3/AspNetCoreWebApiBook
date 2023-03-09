using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardGameList.Data;
using MyBoardGameList.Entities;
using MyBoardGameList.Models;

namespace MyBoardGameList.Controllers;

[Route("boardgames")]
[ApiController]
public class BoardGamesController : ControllerBase
{
    private readonly ILogger<BoardGamesController> _logger;
    private readonly ApplicationDbContext _context;

    public BoardGamesController(ILogger<BoardGamesController> logger, ApplicationDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet(Name = "GetBoardGames")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    public async Task<RestModel<BoardGame[]>> GetBoardGames(int pageIndex = 0, int pageSize = 10)
    {
        var games = await _context.BoardGames
            .AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .OrderBy(g => g.Name)
            .ToArrayAsync();

        return new RestModel<BoardGame[]>
        {
            Data = games,
            Links = new[]
            {
                new LinkModel
                {
                    Href = Url.Action("GetBoardGames", "BoardGames", new { pageIndex, pageSize }, Request.Scheme)!,
                    Rel = "Self",
                    Type = HttpMethod.Get.Method
                }
            },
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalCount = await _context.BoardGames.CountAsync()
        };
    }
}
