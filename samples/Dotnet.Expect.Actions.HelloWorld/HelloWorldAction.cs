namespace Dotnet.Expect.Actions.HelloWorld
{
    using Core.Abstractions;

    public class HelloWorldAction : Action
    {
        public HelloWorldAction(ActionContext context) : base(context)
        {
        }

        public string Output { get; set; }
    }
}