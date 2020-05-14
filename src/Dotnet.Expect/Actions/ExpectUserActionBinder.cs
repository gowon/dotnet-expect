namespace Dotnet.Expect.Actions
{
    using System;
    using Core.Abstractions;

    public class ExpectUserActionBinder : IActionBinder
    {
        public bool CanBind(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Name.Equals("expect-user", StringComparison.OrdinalIgnoreCase);
        }

        public object Bind(IActionDescriptor actionDescriptor, ActionContext context)
        {
            var action = new ExpectUserAction(context)
            {
                Pattern = actionDescriptor.GetRequired(nameof(ExpectUserAction.Pattern))
            };

            var maskedInput = actionDescriptor.GetOptional("masked-input") ?? "false";
            action.MaskedInput = bool.Parse(maskedInput);
            action.CacheKey = actionDescriptor.GetRequired("assign");
            //action.InputType = actionDescriptor.GetOptional("type");

            return action;
        }
    }
}