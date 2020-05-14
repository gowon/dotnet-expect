namespace Dotnet.Expect.Plugins.HelloWorld
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;

    public static class SessionExtensions
    {
        public static async Task HelloWorldAsync(this Session session, string output,
            CancellationToken cancellationToken = default)
        {
            var action =
                new HelloWorldAction(new ActionContext(session.Expectable, session.ItemsCache));

            await new HelloWorldActionHandler().Handle(action, cancellationToken);
        }

        public static void HelloWorld(this Session session, string output)
        {
            //https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md#avoid-using-taskresult-and-taskwait
            Task.Run(() => session.HelloWorldAsync(output)).GetAwaiter().GetResult();
        }
    }
}