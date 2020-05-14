namespace Dotnet.Expect.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Actions;
    using Core.Abstractions;
    using Stubble.Core;
    using Stubble.Core.Builders;

    public static class SessionExtensions
    {
        private static readonly StubbleVisitorRenderer Renderer = new StubbleBuilder().Build();

        public static async Task SendAsync(this Session session, string output,
            CancellationToken cancellationToken = default)
        {
            var action = new SendAction(new ActionContext(session.Expectable, session.ItemsCache))
            {
                Output = output
            };

            await new SendActionHandler(Renderer).Handle(action, cancellationToken);
        }

        public static void Send(this Session session, string output)
        {
            //https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md#avoid-using-taskresult-and-taskwait
            Task.Run(() => session.SendAsync(output)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static async Task ExpectAsync(this Session session, string pattern, Match match, Action<string> handler,
            CancellationToken cancellationToken = default)
        {
            var action = new ExpectAction(new ActionContext(session.Expectable, session.ItemsCache))
            {
                Pattern = pattern,
                Match = match,
                Handler = handler
            };

            await new ExpectActionHandler().Handle(action, cancellationToken).ConfigureAwait(false);
        }
    }
}