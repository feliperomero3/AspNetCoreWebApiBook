using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;
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

    public static void ConfigureSwaggerAction(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter the JWT",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        });

        options.ParameterFilter<SortOrderFilter>();
    }

    public static void ConfigureAuthorization(this AuthorizationOptions options)
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    }
}
