using Microsoft.EntityFrameworkCore;
using MyBoardGameList.Data;
using MyBoardGameList.OpenAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.ParameterFilter<SortOrderFilter>());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
    options.AddPolicy(name: "AnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ApplicationDbContextInitializer>();

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

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
