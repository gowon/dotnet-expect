namespace Dotnet.Expect.Actions
{
    using Core.Abstractions;

    public class ExpectUserAction : Action
    {
        public ExpectUserAction(ActionContext context) : base(context)
        {
        }

        public string Pattern { get; set; }
        public Match Match { get; set; }
        public string CacheKey { get; set; }
        public bool MaskedInput { get; set; }
        public string InputType { get; set; }
    }
}