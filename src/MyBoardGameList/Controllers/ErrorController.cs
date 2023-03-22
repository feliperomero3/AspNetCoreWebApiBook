using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyBoardGameList.Controllers;

[Route("error")]
[ApiController]
public class ErrorController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(IHttpContextAccessor httpContextAccessor, ILogger<ErrorController> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore = true)]
    public ActionResult GetError()
    {
        var httpContext = _httpContextAccessor.HttpContext!;

        var exceptionHandler = httpContext.Features.Get<IExceptionHandlerPathFeature>();

        _logger.LogError(exceptionHandler?.Error, "Error");

        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Detail = exceptionHandler?.Error.Message,
        };

        details.Extensions["traceId"] = System.Diagnostics.Activity.Current?.Id ?? httpContext.TraceIdentifier;

        return new ObjectResult(details);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore = true)]
    [Route("test")]
    public ActionResult GetTestError()
    {
        throw new Exception("Test");
    }
}
