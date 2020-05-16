namespace Dotnet.Expect.Actions.Mustache
{
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using Stubble.Core;

    public class SendFormatActionHandler : ActionHandler<SendFormatAction>
    {
        private readonly StubbleVisitorRenderer _renderer;

        public SendFormatActionHandler(StubbleVisitorRenderer renderer)
        {
            _renderer = renderer;
        }

        public override Task Handle(SendFormatAction request, CancellationToken cancellationToken)
        {
            var context = request.Context;
            var output = Regex.Unescape(request.Output);
            var rendered = _renderer.Render(output, context.ItemsCache);
            context.Expectable.Write(rendered);

            return Task.CompletedTask;
        }
    }
}