namespace Dotnet.Expect.Plugins.HelloWorld
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;

    public class HelloWorldActionHandler : ActionHandler<HelloWorldAction>
    {
        public override Task Handle(HelloWorldAction request, CancellationToken cancellationToken)
        {
            var context = request.Context;
            var output = Regex.Unescape(request.Output);
            //var rendered = context.Renderer.Render(output, context.ItemsCache);
            Console.Write("Hello World: " + output);

            return Task.CompletedTask;
        }
    }
}