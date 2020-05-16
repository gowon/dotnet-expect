namespace Dotnet.Expect.Actions.Mustache
{
    using System;
    using Core.Abstractions;

    public class SendUserFormatActionBinder : IActionBinder
    {
        public bool CanBind(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Name.Equals("send-user-format", StringComparison.OrdinalIgnoreCase);
        }

        public object Bind(IActionDescriptor actionDescriptor, ActionContext context)
        {
            var action = new SendUserFormatAction(context)
            {
                Output = actionDescriptor.GetRequired(nameof(SendUserFormatAction.Output))
            };

            return action;
        }
    }
}