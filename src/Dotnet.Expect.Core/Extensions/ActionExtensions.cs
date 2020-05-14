namespace Dotnet.Expect.Core.Extensions
{
    using System;
    using Abstractions;
    using Action = Abstractions.Action;

    public static class ActionExtensions
    {
        public static bool Is<TAction>(this IAction action)
            where TAction : Action
        {
            return action is TAction;
        }

        public static TAction As<TAction>(this IAction action)
            where TAction : Action
        {
            if (action is TAction @as)
            {
                return @as;
            }

            throw new InvalidCastException($"Action is not of type '{typeof(TAction).FullName}'");
        }
    }
}