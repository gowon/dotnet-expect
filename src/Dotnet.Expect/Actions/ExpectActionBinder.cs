namespace Dotnet.Expect.Actions
{
    using System;
    using Core.Abstractions;

    public class ExpectActionBinder : IActionBinder
    {
        public bool CanBind(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Name.Equals("expect", StringComparison.OrdinalIgnoreCase);
        }

        public object Bind(IActionDescriptor actionDescriptor, ActionContext context)
        {
            var action = new ExpectAction(context)
            {
                Pattern = actionDescriptor.GetRequired(nameof(ExpectAction.Pattern)),
                Match = Match.Contains
            };

            if (Enum.TryParse(actionDescriptor.GetOptional(nameof(ExpectAction.Match)), true, out Match match))
            {
                action.Match = match;
            }

            var defaultTimeoutValue = (int) context.ItemsCache[nameof(ISessionDescriptor.DefaultTimeout)];
            var timeoutValue = actionDescriptor.GetOptional(nameof(ExpectAction.Timeout));
            action.Timeout = timeoutValue != null ? int.Parse(timeoutValue) : defaultTimeoutValue;
            action.CacheKey = actionDescriptor.GetOptional("assign");

            return action;
        }
    }
}