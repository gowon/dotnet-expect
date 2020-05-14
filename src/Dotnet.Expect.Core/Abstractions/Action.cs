namespace Dotnet.Expect.Core.Abstractions
{
    using System;

    public abstract class Action : IAction
    {
        protected Action(ActionContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ActionContext Context { get; }
    }
}