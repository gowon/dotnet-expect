namespace Dotnet.Expect.Plugins.HelloWorld
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