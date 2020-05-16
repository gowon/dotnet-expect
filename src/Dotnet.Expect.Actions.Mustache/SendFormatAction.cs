namespace Dotnet.Expect.Actions.Mustache
{
    using Core.Abstractions;

    public class SendFormatAction : Action
    {
        public SendFormatAction(ActionContext context) : base(context)
        {
        }

        public string Output { get; set; }
    }
}