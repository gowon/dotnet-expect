namespace Dotnet.Expect.Core.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IActionHandler
    {
        bool CanHandle(IAction action);
        Task Handle(IAction action, CancellationToken cancellationToken);
    }
}