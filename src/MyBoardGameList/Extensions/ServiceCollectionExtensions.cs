using Microsoft.AspNetCore.Cors.Infrastructure;
using MyBoardGameList.OpenAPI;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyBoardGameList.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureCorsAction(CorsOptions options)
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        options.AddPolicy("AnyOrigin",
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
    }

    public static void ConfigureSwaggerAction(SwaggerGenOptions options) => options.ParameterFilter<SortOrderFilter>();
}
