namespace Dotnet.Expect.Actions
{
    using System;
    using Core.Abstractions;

    public class SendActionBinder : IActionBinder
    {
        public bool CanBind(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Name.Equals("send", StringComparison.OrdinalIgnoreCase);
        }

        public object Bind(IActionDescriptor actionDescriptor, ActionContext context)
        {
            var action = new SendAction(context)
            {
                Output = actionDescriptor.GetRequired(nameof(SendAction.Output))
            };

            return action;
        }
    }
}