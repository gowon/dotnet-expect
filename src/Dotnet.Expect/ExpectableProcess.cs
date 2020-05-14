namespace Dotnet.Expect
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Core.Abstractions;

    /// <summary>
    ///     Represents spawnable shell command
    /// </summary>
    public class ExpectableProcess : IExpectable
    {
        private bool _disposed;
        private Task<string> _errorRead;
        private Task<string> _stdRead;

        /// <summary>
        ///     Initializes new ProcessSpawnable instance to handle shell command process
        /// </summary>
        /// <param name="filename">filename to be run</param>
        /// <param name="arguments">arguments to be passed to process</param>
        public ExpectableProcess(string filename, string arguments)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Filename cannot be empty string", nameof(filename));
            }

            var process = new Process {StartInfo = {FileName = filename, Arguments = arguments}};
            Process = process;
        }

        /// <summary>
        ///     Initializes new ProcessSpawnable instance to handle shell command process
        /// </summary>
        /// <param name="filename">filename to be run</param>
        public ExpectableProcess(string filename) : this(filename, string.Empty)
        {
        }

        public ExpectableProcess(Process process)
        {
            Process = process;
        }

        public Process Process { get; }

        /// <summary>
        ///     Prepares and starts process
        /// </summary>
        public void Start()
        {
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.RedirectStandardError = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.Start();
        }

        /// <summary>
        ///     Writes to process StandardInput stream
        /// </summary>
        /// <param name="command">specify what should be written to process</param>
        public void Write(string command)
        {
            if (_errorRead == null || _errorRead.IsCanceled || _errorRead.IsCompleted || _errorRead.IsFaulted)
            {
                Process.StandardError.DiscardBufferedData();
            }

            if (_stdRead == null || _stdRead.IsCanceled || _stdRead.IsCompleted || _stdRead.IsFaulted)
            {
                Process.StandardOutput.DiscardBufferedData();
            }

            Process.StandardInput.Write(command);
        }

        /// <summary>
        ///     Reads in asynchronous way from both standard input and standard error streams.
        /// </summary>
        /// <returns>text read from streams</returns>
        public async Task<string> ReadAsync()
        {
            var tasks = new List<Task<string>>();
            RecreateErrorReadTask();
            RecreateStdReadTask();
            tasks.Add(_errorRead);
            tasks.Add(_stdRead);

            var result = await Task.WhenAny(tasks).ConfigureAwait(false);
            return await result.ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void RecreateErrorReadTask()
        {
            if (_errorRead == null || _errorRead.IsCanceled || _errorRead.IsCompleted || _errorRead.IsFaulted)
            {
                var buffer = new char[256];
                _errorRead = CreateStringAsync(buffer, Process.StandardError.ReadAsync(buffer, 0, buffer.Length));
            }
        }

        private void RecreateStdReadTask()
        {
            if (_stdRead == null || _stdRead.IsCanceled || _stdRead.IsCompleted || _stdRead.IsFaulted)
            {
                var buffer = new char[256];
                _stdRead = CreateStringAsync(buffer, Process.StandardOutput.ReadAsync(buffer, 0, buffer.Length));
            }
        }

        private async Task<string> CreateStringAsync(char[] c, Task<int> n)
        {
            return new string(c, 0, await n.ConfigureAwait(false));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Process?.Dispose();
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }
    }
}