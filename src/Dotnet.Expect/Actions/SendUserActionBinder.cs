namespace Dotnet.Expect.Actions
{
    using System;
    using Core.Abstractions;

    public class SendUserActionBinder : IActionBinder
    {
        public bool CanBind(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Name.Equals("send-user", StringComparison.OrdinalIgnoreCase);
        }

        public object Bind(IActionDescriptor actionDescriptor, ActionContext context)
        {
            var action = new SendUserAction(context)
            {
                Output = actionDescriptor.GetRequired(nameof(SendUserAction.Output))
            };

            return action;
        }
    }
}