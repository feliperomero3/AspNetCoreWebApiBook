using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MyBoardGameList.Data;
using MyBoardGameList.Extensions;
using MyBoardGameList.OpenAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(MvcBuilderExtensions.ConfigureMvcAction);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.ParameterFilter<SortOrderFilter>());

builder.Services.AddCors(ServiceCollectionExtensions.ConfigureCorsAction);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ApplicationDbContextInitializer>();

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 16 * 1024 * 1024;
    options.SizeLimit = 32 * 1024 * 1024;
});

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>().Initialize();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(action =>
    {
        action.Run(async context =>
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
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.Use((context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new CacheControlHeaderValue
        {
            NoCache = true,
            NoStore = true,
            MustRevalidate = true,
            Private = true,
            MaxAge = TimeSpan.FromSeconds(0)
        };

    return next();
});

app.UseResponseCaching();

app.MapControllers();

app.Run();
