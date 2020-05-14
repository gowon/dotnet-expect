namespace Dotnet.Expect.Core.Abstractions
{
    public interface IActionBinder
    {
        bool CanBind(IActionDescriptor actionDescriptor);
        object Bind(IActionDescriptor actionDescriptor, ActionContext context);
    }
}