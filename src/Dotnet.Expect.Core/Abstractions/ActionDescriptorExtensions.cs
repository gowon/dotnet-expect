namespace Dotnet.Expect.Core.Abstractions
{
    using System.Collections.Generic;

    public static class ActionDescriptorExtensions
    {
        public static string GetOptional(this IActionDescriptor actionDescriptor, string key)
        {
            return actionDescriptor.Properties.TryGetValue(key.ToLowerInvariant(), out var value) ? value : null;
        }

        public static string GetRequired(this IActionDescriptor actionDescriptor, string key)
        {
            if (!actionDescriptor.Properties.TryGetValue(key.ToLowerInvariant(), out var value))
            {
                throw new KeyNotFoundException(key);
            }

            return value;
        }
    }
}