namespace Dotnet.Expect.Scripting.Yaml
{
    using System;
    using System.IO;
    using Core.Abstractions;
    using YamlDotNet.Core;

    public static class SessionDescriptorFactoryExtensions
    {
        public static ISessionDescriptor CreateFromYamlFile(this ISessionDescriptorProvider provider, string path)
        {
            if (!(provider is YamlSessionDescriptorProvider yamlFactory))
            {
                throw new InvalidOperationException(
                    $"Provider must be of type '{nameof(YamlSessionDescriptorProvider)}'.");
            }

            var input = File.ReadAllText(path);
            return yamlFactory.Create(input);
        }

        public static ISessionDescriptor CreateFromYamlFile(this ISessionDescriptorProvider provider, TextReader reader)
        {
            if (!(provider is YamlSessionDescriptorProvider yamlFactory))
            {
                throw new InvalidOperationException(
                    $"Provider must be of type '{nameof(YamlSessionDescriptorProvider)}'.");
            }

            return yamlFactory.Create(reader);
        }

        public static ISessionDescriptor CreateFromYamlFile(this ISessionDescriptorProvider provider, IParser parser)
        {
            if (!(provider is YamlSessionDescriptorProvider yamlFactory))
            {
                throw new InvalidOperationException(
                    $"Provider must be of type '{nameof(YamlSessionDescriptorProvider)}'.");
            }

            return yamlFactory.Create(parser);
        }
    }
}