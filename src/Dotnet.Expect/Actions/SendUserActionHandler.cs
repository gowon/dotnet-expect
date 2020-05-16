namespace Dotnet.Expect.Actions
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;

    public class SendUserActionHandler : ActionHandler<SendUserAction>
    {
        public override Task Handle(SendUserAction request, CancellationToken cancellationToken)
        {
            var output = Regex.Unescape(request.Output);
            Console.Write(output);

            return Task.CompletedTask;
        }
    }
}