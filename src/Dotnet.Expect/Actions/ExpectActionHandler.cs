namespace Dotnet.Expect.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;

    public class ExpectActionHandler : ActionHandler<ExpectAction>
    {
        public override async Task Handle(ExpectAction request, CancellationToken cancellationToken)
        {
            var timeout = request.Timeout;
            Task timeoutTask = null;
            if (timeout > 0)
            {
                timeoutTask = Task.Delay(timeout, cancellationToken);
            }

            var output = "";
            var expectedQueryFound = false;
            while (!cancellationToken.IsCancellationRequested && !expectedQueryFound)
            {
                var task = request.Context.Expectable.ReadAsync();
                IList<Task> tasks = new List<Task>();
                tasks.Add(task);
                if (timeoutTask != null)
                {
                    tasks.Add(timeoutTask);
                }

                var any = await Task.WhenAny(tasks).ConfigureAwait(false);
                if (task == any)
                {
                    output += await task.ConfigureAwait(false);
                    var pattern = request.Pattern;

                    switch (request.Match)
                    {
                        case Match.Contains:
                            expectedQueryFound = output.Contains(pattern);
                            break;
                        case Match.Regex:
                            foreach (var line in output.Split(new[] {Environment.NewLine}, StringSplitOptions.None))
                            {
                                var match = Regex.Match(line, pattern);
                                if (match.Success)
                                {
                                    expectedQueryFound = match.Success;
                                }
                            }

                            break;
                    }

                    if (expectedQueryFound)
                    {
                        request.Handler?.Invoke(output);
                    }
                }
                else
                {
                    throw new TimeoutException();
                }
            }
        }
    }
}