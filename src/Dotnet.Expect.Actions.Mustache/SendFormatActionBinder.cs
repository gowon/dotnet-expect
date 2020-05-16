namespace Dotnet.Expect.Actions.Mustache
{
    using System;
    using Core.Abstractions;

    public class SendFormatActionBinder : IActionBinder
    {
        public bool CanBind(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Name.Equals("send-format", StringComparison.OrdinalIgnoreCase);
        }

        public object Bind(IActionDescriptor actionDescriptor, ActionContext context)
        {
            var action = new SendFormatAction(context)
            {
                Output = actionDescriptor.GetRequired(nameof(SendFormatAction.Output))
            };

            return action;
        }
    }
}