namespace Dotnet.Expect
{
    using Core.Abstractions;

    public static class Expect
    {
        /// <summary>
        ///     Spawn new session using the default process wrapper
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Session Spawn(string filename)
        {
            return Spawn(filename, string.Empty);
        }

        /// <summary>
        ///     Spawn new session using the default process wrapper
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Session Spawn(string filename, string arguments)
        {
            var process = new ExpectableProcess(filename, arguments);
            return Spawn(process);
        }

        public static Session Spawn(IExpectable expectable)
        {
            expectable.Start();
            return new Session(expectable);
        }
    }
}