namespace Dotnet.Expect.Actions.Mustache
{
    using Core.Abstractions;

    public class SendUserFormatAction : Action
    {
        public SendUserFormatAction(ActionContext context) : base(context)
        {
        }

        public string Output { get; set; }
    }
}