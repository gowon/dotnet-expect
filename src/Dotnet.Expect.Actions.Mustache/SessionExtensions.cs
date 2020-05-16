namespace Dotnet.Expect.Actions.Mustache
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using Stubble.Core;
    using Stubble.Core.Builders;

    public static class SessionExtensions
    {
        private static readonly StubbleVisitorRenderer Renderer = new StubbleBuilder().Build();

        public static async Task SendAsync(this Session session, string output,
            CancellationToken cancellationToken = default)
        {
            var action = new SendFormatAction(new ActionContext(session.Expectable, session.ItemsCache))
            {
                Output = output
            };

            await new SendFormatActionHandler(Renderer).Handle(action, cancellationToken);
        }

        public static void Send(this Session session, string output)
        {
            //https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md#avoid-using-taskresult-and-taskwait
            Task.Run(() => session.SendAsync(output)).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}