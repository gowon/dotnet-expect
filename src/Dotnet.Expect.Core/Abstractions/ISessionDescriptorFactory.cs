namespace Dotnet.Expect.Core.Abstractions
{
    public interface ISessionDescriptorFactory
    {
        ISessionDescriptor Create(params object[] args);
    }
}