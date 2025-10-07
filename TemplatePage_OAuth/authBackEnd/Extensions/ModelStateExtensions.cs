using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace authBackEnd.Extensions;

public static class ModelStateExtensions
{
    public static string GetFullErrorMessage(this ModelStateDictionary modelState)
    {
        var messages = new List<string>();

        foreach (var entry in modelState)
        {
            foreach (var error in entry.Value.Errors)
            {
                messages.Add(error.ErrorMessage);
            }
        }

        return string.Join(" ", messages);
    }
}
