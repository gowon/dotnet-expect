namespace Dotnet.Expect.Actions
{
    using System;
    using Core.Abstractions;
    using Action = Core.Abstractions.Action;

    public class ExpectAction : Action
    {
        public ExpectAction(ActionContext context) : base(context)
        {
        }

        public string Pattern { get; set; }
        public Match Match { get; set; }
        public bool CacheOnMatch { get; set; }
        public string CacheKey { get; set; }
        public Action<string> Handler { get; set; }
        public int Timeout { get; set; }
    }
}