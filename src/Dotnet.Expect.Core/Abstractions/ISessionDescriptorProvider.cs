namespace Dotnet.Expect.Core.Abstractions
{
    public interface ISessionDescriptorProvider
    {
        ISessionDescriptor Create(params object[] args);
    }
}