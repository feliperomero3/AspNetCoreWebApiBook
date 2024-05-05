using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyBoardGameList.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder builder)
    {
        builder.Run(async context =>
        {
            var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();

            var details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = exceptionHandler?.Error.Message,
                Extensions =
                {
                    ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier
                }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(details));
        });
    }
}
