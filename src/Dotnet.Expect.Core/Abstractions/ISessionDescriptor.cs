namespace Dotnet.Expect.Core.Abstractions
{
    using System.Collections.Generic;

    public interface ISessionDescriptor
    {
        string Name { get; }
        string Path { get; }
        string Arguments { get; }
        int DefaultTimeout { get; }
        List<IActionDescriptor> ActionDescriptors { get; }
    }
}
