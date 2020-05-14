namespace Dotnet.Expect.Actions
{
    using Core.Abstractions;

    public class SendUserAction : Action
    {
        public SendUserAction(ActionContext context) : base(context)
        {
        }

        public string Output { get; set; }
    }
}