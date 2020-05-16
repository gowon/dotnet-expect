namespace Dotnet.Expect.Actions.HelloWorld
{
    using System;
    using Core.Abstractions;

    public class HelloWorldActionBinder : IActionBinder
    {
        public bool CanBind(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Name.Equals("hello-world", StringComparison.OrdinalIgnoreCase);
        }

        public object Bind(IActionDescriptor actionDescriptor, ActionContext context)
        {
            var action = new HelloWorldAction(context)
            {
                Output = actionDescriptor.GetRequired(nameof(HelloWorldAction.Output))
            };

            return action;
        }
    }
}