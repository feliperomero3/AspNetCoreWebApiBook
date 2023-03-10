using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardGameList.Data;
using MyBoardGameList.Entities;
using MyBoardGameList.Models;
using MyBoardGameList.Validators;

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
    public async Task<PagedRestModel<BoardGame[]>> GetBoardGames(
        [MaxLength(int.MaxValue)] int pageIndex = 0,
        [Range(1, 100)] int pageSize = 10,
        [SortOrderValidator] string? sortOrder = "ASC",
        [StringLength(64)] string? filterQuery = null)
    {
        var query = _context.BoardGames.AsNoTracking();
        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(filterQuery))
        {
            query = query.Where(b => b.Name.Contains(filterQuery));
        }

        query = sortOrder == "ASC" ? query.OrderBy(g => g.Name) : query.OrderByDescending(g => g.Name);

        var games = await query.Skip(pageIndex * pageSize).Take(pageSize).ToArrayAsync();

        return new PagedRestModel<BoardGame[]>
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
            TotalCount = totalCount
        };
    }

    [HttpPost("{id}", Name = "UpdateBoardGame")]
    [ResponseCache(NoStore = true)]
    public async Task<ActionResult<RestModel<BoardGame>>> UpdateBoardGame(int id, InputBoardGameModel boardGameModel)
    {
        var boardGame = await _context.BoardGames.FindAsync(id);

        if (boardGame == null)
        {
            ModelState.AddModelError(string.Empty, "Board game specified doesn't exist.");
            return UnprocessableEntity(ModelState);
        }

        _context.Entry(boardGame).CurrentValues.SetValues(boardGameModel);

        await _context.SaveChangesAsync();

        var result = new RestModel<BoardGame[]>
        {
            Data = new[] { boardGame },
            Links = new[]
            {
                new LinkModel
                {
                    Href = Url.Action("UpdateBoardGame", "BoardGames", new { id }, Request.Scheme)!,
                    Rel = "Self",
                    Type = Request.Method
                }
            }
        };

        return Ok(result);
    }

    [HttpDelete("{id}", Name = "DeleteBoardGame")]
    [ResponseCache(NoStore = true)]
    public async Task<ActionResult> DeleteGame(int id)
    {
        var boardgame = await _context.BoardGames.FindAsync(id);

        if (boardgame == null)
        {
            return NoContent();
        }

        _context.BoardGames.Remove(boardgame);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
