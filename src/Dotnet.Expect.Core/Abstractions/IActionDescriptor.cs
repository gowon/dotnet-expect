namespace Dotnet.Expect.Core.Abstractions
{
    using System.Collections.Generic;

    public interface IActionDescriptor
    {
        string Name { get; }
        Dictionary<string, string> Properties { get; }
    }
}