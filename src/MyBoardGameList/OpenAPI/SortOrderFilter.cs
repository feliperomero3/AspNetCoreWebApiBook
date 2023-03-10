using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MyBoardGameList.Validators;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyBoardGameList.OpenAPI;

public class SortOrderFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        var attributes = context.ParameterInfo?
                        .GetCustomAttributes(true)
                        .OfType<SortOrderValidatorAttribute>();

        if (attributes != null)
        {
            foreach (var attribute in attributes)
            {
                parameter.Schema.Extensions.Add("pattern", new OpenApiString(string.Join("|", attribute.AllowedValues)));
            }
        }
    }
}
