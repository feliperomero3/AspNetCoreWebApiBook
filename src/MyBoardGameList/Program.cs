using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddAuthorization();

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

app.UseResponseCaching();

app.MapControllers();

app.Run();
