namespace Dotnet.Expect.Core.Abstractions
{
    using System;

    public class Session : IDisposable
    {
        private int _defaultTimeout = 2500;
        private bool _disposed;

        public Session(IExpectable expectable)
        {
            Expectable = expectable;
        }

        public IExpectable Expectable { get; }
        public ItemsCache ItemsCache { set; get; }

        /// <summary>
        ///     Timeout value in miliseconds for Expect function
        /// </summary>
        public int DefaultTimeout
        {
            get => _defaultTimeout;


            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Value must be larger than zero");
                }

                _defaultTimeout = value;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Expectable?.Dispose();
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }
    }
}