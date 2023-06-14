using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MyBoardGameList.Controllers;

[Route("error")]
[ApiController]
public class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [EnableCors("AnyOrigin")]
    [Route("test")]
    public ActionResult GetTestError()
    {
        throw new Exception("Test");
    }
}
