using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardGameList.Data;
using MyBoardGameList.Models;

namespace MyBoardGameList.Controllers;
[Route("domains")]
[ApiController]
public class DomainsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DomainsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetDomains")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    public async Task<ActionResult<PagedRestModel<DomainModel[]>>> GetDomains([FromQuery] RequestModel model)
    {
        if (!ModelState.IsValid)
        {
            var details = new ValidationProblemDetails(ModelState);
            details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            if (ModelState.Keys.Any(k => k == "PageSize"))
            {
                details.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.2";
                details.Status = StatusCodes.Status501NotImplemented;

                return new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status501NotImplemented
                };
            }
            else
            {
                details.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;

                return new BadRequestObjectResult(details);
            }
        }

        var query = _context.Domains.AsNoTracking();
        var totalCount = query.Count();

        if (!string.IsNullOrEmpty(model.FilterQuery))
        {
            query = query.Where(b => b.Name.Contains(model.FilterQuery));
        }

        query = model.SortOrder == "ASC" ? query.OrderBy(g => g.Name) : query.OrderByDescending(g => g.Name);

        var domains = await query
            .Skip(model.PageIndex * model.PageSize)
            .Take(model.PageSize)
            .Select(d => new DomainModel
            {
                Id = d.Id,
                Name = d.Name,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            })
            .ToArrayAsync();

        var responseModel = new PagedRestModel<DomainModel[]>
        {
            Data = domains,
            Links = new[]
            {
                new LinkModel
                {
                    Href = Url.Action("GetDomains", "Domains", new { model.PageIndex, model.PageSize }, Request.Scheme)!,
                    Rel = "Self",
                    Type = HttpMethod.Get.Method
                }
            },
            PageIndex = model.PageIndex,
            PageSize = model.PageSize,
            TotalCount = totalCount
        };

        return Ok(responseModel);
    }
}
