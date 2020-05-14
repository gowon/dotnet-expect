namespace Dotnet.Expect.Scripting.Yaml
{
    using System;
    using System.IO;
    using Core.Abstractions;

    public static class SessionDescriptorFactoryExtensions
    {
        public static ISessionDescriptor CreateFromYamlFile(this ISessionDescriptorFactory factory, string path)
        {
            if (!(factory is YamlSessionDescriptorFactory yamlFactory))
            {
                throw new InvalidOperationException(
                    $"Factory must be of type '{nameof(YamlSessionDescriptorFactory)}'.");
            }

            var input = File.ReadAllText(path);
            return yamlFactory.Create(input);
        }
    }
}