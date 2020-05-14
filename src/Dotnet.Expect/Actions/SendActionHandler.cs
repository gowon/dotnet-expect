namespace Dotnet.Expect.Actions
{
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using Stubble.Core;

    public class SendActionHandler : ActionHandler<SendAction>
    {
        private readonly StubbleVisitorRenderer _renderer;

        public SendActionHandler(StubbleVisitorRenderer renderer)
        {
            _renderer = renderer;
        }

        public override Task Handle(SendAction request, CancellationToken cancellationToken)
        {
            var context = request.Context;
            var output = Regex.Unescape(request.Output);
            var rendered = _renderer.Render(output, context.ItemsCache);
            context.Expectable.Write(rendered);

            return Task.CompletedTask;
        }
    }
}