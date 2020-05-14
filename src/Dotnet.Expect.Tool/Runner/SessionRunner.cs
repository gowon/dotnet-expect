namespace Dotnet.Expect.Tool.Runner
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Core.Abstractions;
    using Dotnet.Expect;

    public class SessionRunner
    {
        private readonly List<IActionBinder> _actionBinders;
        private readonly List<IActionHandler> _actionHandlers;

        public SessionRunner(IEnumerable<IActionHandler> actionHandlers, IEnumerable<IActionBinder> actionBinders)
        {
            _actionHandlers = actionHandlers.ToList();
            _actionBinders = actionBinders.ToList();
        }

        public void Execute(ISessionDescriptor sessionDescriptor)
        {
            using (var session = Expect.Spawn(sessionDescriptor.Path, sessionDescriptor.Arguments))
            {
                session.DefaultTimeout = sessionDescriptor.DefaultTimeout;
                session.ItemsCache = new ItemsCache
                {
                    {nameof(ISessionDescriptor.DefaultTimeout), sessionDescriptor.DefaultTimeout}
                };

                foreach (var actionDescriptor in sessionDescriptor.ActionDescriptors)
                {
                    var binder = _actionBinders.First(actionBinder => actionBinder.CanBind(actionDescriptor));
                    var actionContext = new ActionContext(session.Expectable, session.ItemsCache);
                    var action = (IAction) binder.Bind(actionDescriptor, actionContext);


                    var handler = _actionHandlers.First(actionBinder => actionBinder.CanHandle(action));

                    handler.Handle(action, CancellationToken.None);
                }
            }
        }
    }
}