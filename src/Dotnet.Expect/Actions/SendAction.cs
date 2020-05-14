namespace Dotnet.Expect.Actions
{
    using Core.Abstractions;

    public class SendAction : Action
    {
        public SendAction(ActionContext context) : base(context)
        {
        }

        public string Output { get; set; }
    }
}