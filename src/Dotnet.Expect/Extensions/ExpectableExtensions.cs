namespace Dotnet.Expect.Extensions
{
    using System.Threading.Tasks;
    using Core.Abstractions;

    public static class ExpectableExtensions
    {
        /// <summary>
        ///     Reads in synchronous way from both standard input and standard error streams.
        /// </summary>
        /// <returns>text read from streams</returns>
        public static string Read(this IExpectable expectable)
        {
            var task = Task.Run(async () => await expectable.ReadAsync().ConfigureAwait(false));
            return task.Result;
        }
    }
}