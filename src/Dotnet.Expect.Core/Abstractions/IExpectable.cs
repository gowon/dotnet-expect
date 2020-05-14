namespace Dotnet.Expect.Core.Abstractions
{
    using System;
    using System.Threading.Tasks;

    public interface IExpectable : IDisposable
    {
        void Start();
        void Write(string command);
        //string Read();
        Task<string> ReadAsync();
    }
}
