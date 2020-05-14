namespace Dotnet.Expect.Scripting.Yaml
{
    using System.Collections.Generic;
    using Core.Abstractions;
    using YamlDotNet.Serialization;

    public class YamlActionDescriptor : IActionDescriptor
    {
        [YamlMember(Alias = "action")]
        public string Name { get; set; }

        [YamlMember(Alias = "with")]
        public Dictionary<string, string> Properties { get; set; }
    }
}