using Microsoft.AspNetCore.Mvc;

namespace MyBoardGameList.Extensions;

public static class MvcBuilderExtensions
{
    public static void ConfigureMvcAction(MvcOptions options)
    {
        options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
            x => $"The value '{x}' is invalid.");
        options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
            x => $"The field {x} must be a number.");
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
            (x, y) => $"The value '{x}' is not valid for {y}.");
        options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
            () => "A value is required.");

        options.CacheProfiles.Add("NoCache",
            new CacheProfile
            {
                NoStore = true
            });
        options.CacheProfiles.Add("Any-60",
            new CacheProfile
            {
                Location = ResponseCacheLocation.Any,
                Duration = 60
            });
    }
}
