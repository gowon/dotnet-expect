namespace Dotnet.Expect.Scripting.Yaml
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Abstractions;
    using YamlDotNet.Serialization;

    public class YamlSessionDescriptor : ISessionDescriptor
    {
        public YamlSessionDescriptor()
        {
            YamlActionDescriptors = new List<YamlActionDescriptor>();
            DefaultTimeout = 5000;
        }

        [YamlMember(Alias = "steps")]
        public List<YamlActionDescriptor> YamlActionDescriptors { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public int DefaultTimeout { get; set; }

        [YamlMember(Alias = "args")]
        public string Arguments { get; set; }

        [YamlIgnore]
        public List<IActionDescriptor> ActionDescriptors => YamlActionDescriptors.Cast<IActionDescriptor>().ToList();
    }
}