namespace Dotnet.Expect.Actions
{
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;

    public class SendActionHandler : ActionHandler<SendAction>
    {
        public override Task Handle(SendAction request, CancellationToken cancellationToken)
        {
            var context = request.Context;
            var output = Regex.Unescape(request.Output);
            context.Expectable.Write(output);

            return Task.CompletedTask;
        }
    }
}