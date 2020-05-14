namespace Dotnet.Expect.Core.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;

    public abstract class ActionHandler<TAction> : IActionHandler where TAction : Action
    {
        public virtual bool CanHandle(IAction action)
        {
            return action.Is<TAction>();
        }

        public virtual async Task Handle(IAction action, CancellationToken cancellationToken)
        {
            await Handle(action.As<TAction>(), cancellationToken);
        }

        public abstract Task Handle(TAction action, CancellationToken cancellationToken);
    }
}