namespace Dotnet.Expect.Scripting.Yaml
{
    using System;
    using System.IO;
    using Core.Abstractions;
    using YamlDotNet.Core;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class YamlSessionDescriptorFactory : ISessionDescriptorFactory
    {
        private readonly IDeserializer _deserializer;

        public YamlSessionDescriptorFactory()
        {
            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        public ISessionDescriptor Create(params object[] args)
        {
            switch (args[0])
            {
                case string input:
                    return _deserializer.Deserialize<YamlSessionDescriptor>(input);
                case TextReader reader:
                    return _deserializer.Deserialize<YamlSessionDescriptor>(reader);
                case IParser parser:
                    return _deserializer.Deserialize<YamlSessionDescriptor>(parser);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}