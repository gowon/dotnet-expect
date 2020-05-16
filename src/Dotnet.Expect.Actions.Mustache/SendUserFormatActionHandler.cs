namespace Dotnet.Expect.Actions.Mustache
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using Stubble.Core;

    public class SendUserFormatActionHandler : ActionHandler<SendUserFormatAction>
    {
        private readonly StubbleVisitorRenderer _renderer;

        public SendUserFormatActionHandler(StubbleVisitorRenderer renderer)
        {
            _renderer = renderer;
        }

        public override Task Handle(SendUserFormatAction request, CancellationToken cancellationToken)
        {
            var context = request.Context;
            var output = Regex.Unescape(request.Output);
            var rendered = _renderer.Render(output, context.ItemsCache);
            Console.Write(rendered);

            return Task.CompletedTask;
        }
    }
}