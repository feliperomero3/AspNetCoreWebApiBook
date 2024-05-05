using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MyBoardGameList.Data;
using MyBoardGameList.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(MvcBuilderExtensions.ConfigureMvcAction);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ServiceCollectionExtensions.ConfigureSwaggerAction);

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
    app.UseExceptionHandler(MyBoardGameList.Extensions.ApplicationBuilderExtensions.ConfigureExceptionHandler);
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
