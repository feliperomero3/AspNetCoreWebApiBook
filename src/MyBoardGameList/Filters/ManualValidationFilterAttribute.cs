using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MyBoardGameList.Filters;

/// <summary>
/// Filter attribute that will check for the presence of the ModelStateInvalidFilter within
/// a given action method and remove it.
/// </summary>
/// <remarks>
/// Can be used to conditionally disable the API Controller ModelState auto-validation feature.
/// </remarks>
public class ManualValidationFilterAttribute : Attribute, IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        // It's worth noting that this filter won't affect the security posture of our application,
        // since it will only be used to (conditionally) disable a default security setting
        // if it manages to find it by its name; for that very reason, it shouldn't pose any
        // significant security threat even if it will stop working properly - in the worst-case scenario,
        // it won't do anything.
        for (var i = 0; i < action.Filters.Count; i++)
        {
            // Sadly, the ModelStateInvalidFilterFactory type is marked as internal, which prevents us from
            // checking for the filter presence using a strongly typed approach.
            // We must compare the Name property with the literal name of the class.
            // That's hardly an ideal approach and might cease to work if that name will change
            // in future releases of the framework, but for now it will do the trick.
            // Cannot use -> nameof(ModelStateInvalidFilterFactory)
            if (action.Filters[i] is ModelStateInvalidFilter || action.Filters[i].GetType().Name == "ModelStateInvalidFilterFactory")
            {
                action.Filters.RemoveAt(i);
                break;
            }
        }
    }
}
