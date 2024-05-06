using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MyBoardGameList.Constants;
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
    private readonly IMemoryCache _cache;

    public BoardGamesController(ILogger<BoardGamesController> logger, ApplicationDbContext context, IMemoryCache cache)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    [HttpGet(Name = "GetBoardGames")]
    [ResponseCache(CacheProfileName = "Any-60")]
    public async Task<PagedRestModel<BoardGame[]>> GetBoardGames([FromQuery] RequestModel model)
    {
        _logger.LogInformation("GetBoardGames method started.");

        var cacheKey = $"{model.GetType()}-{JsonSerializer.Serialize(model)}";

        _cache.TryGetValue(cacheKey, out BoardGame[]? cachedGames);

        var query = _context.BoardGames.AsNoTracking();
        var totalCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(model.FilterQuery))
        {
            query = query.Where(b => b.Name.Contains(model.FilterQuery));
        }

        query = model.SortOrder == "ASC" ? query.OrderBy(g => g.Name) : query.OrderByDescending(g => g.Name);

        var games = cachedGames ?? await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToArrayAsync();

        if (cachedGames == null)
        {
            _cache.Set(cacheKey, games, TimeSpan.FromSeconds(120));
        }

        return new PagedRestModel<BoardGame[]>
        {
            Data = games,
            Links = new[]
            {
                new LinkModel
                {
                    Href = Url.Action("GetBoardGames", "BoardGames", new { model.PageIndex, model.PageSize }, Request.Scheme)!,
                    Rel = "Self",
                    Type = HttpMethod.Get.Method
                }
            },
            PageIndex = model.PageIndex,
            PageSize = model.PageSize,
            TotalCount = totalCount
        };
    }

    [HttpPost("{id}", Name = "UpdateBoardGame")]
    [ResponseCache(CacheProfileName = "NoCache")]
    [Authorize(Roles = RoleNames.Moderator)]
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
    [ResponseCache(CacheProfileName = "NoCache")]
    [Authorize(Roles = RoleNames.Administrator)]
    public async Task<ActionResult> DeleteGame(int id)
    {
        Expression<Func<BoardGame, bool>> query = b => b.Id == id;

        if (!await _context.BoardGames.AnyAsync(query))
        {
            return NoContent();
        }

        await _context.BoardGames.Where(query).ExecuteDeleteAsync();

        return NoContent();
    }
}
