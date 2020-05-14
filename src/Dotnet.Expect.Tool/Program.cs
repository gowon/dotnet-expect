namespace Dotnet.Expect.Tool
{
    using System.CommandLine;
    using System.CommandLine.Builder;
    using System.CommandLine.Hosting;
    using System.CommandLine.Invocation;
    using System.CommandLine.Parsing;
    using System.IO;
    using System.Threading.Tasks;
    using Actions;
    using Core.Abstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Runner;
    using Scripting.Yaml;

    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var parser = new CommandLineBuilder(CreateCommandLineInterface())
                .UseDefaults() // adds basic CLI features
                .UseHost(CreateHostBuilder)
                .Build();

            return await parser.InvokeAsync(args);
        }

        private static RootCommand CreateCommandLineInterface()
        {
            var rootCommand = new RootCommand();
            rootCommand.Handler = CommandHandler.Create<IHost>(Run);

            rootCommand.AddArgument(new Argument<FileInfo>("path")
            {
                Description = "Path of the script file to execute.",
                Arity = ArgumentArity.ExactlyOne
            });

            rootCommand.AddOption(new Option<string>("--plugin-path"));
            //rootCommand.AddOption(new Option<int>(new string[] { "--int-option", "/int-option" }, getDefaultValue: () => 42, description: "An option whose argument is parsed to an int"));
            //rootCommand.AddOption(new Option<string>("--string-option", getDefaultValue: () => "NONE", description: "An option for string value"));
            //rootCommand.AddOption(new Option("--that-option", "An option that's there or not"));

            //rootCommand.Description = "Sample App using System.CommandLine";

            return rootCommand;
        }

        private static async Task<int> Run(IHost host)
        {
            var provider = host.Services;
            var factory = provider.GetService<ISessionDescriptorFactory>();
            var runner = provider.GetRequiredService<SessionRunner>();

            var sessionDescriptor = factory.CreateFromYamlFile("expect.yml");
            runner.Execute(sessionDescriptor);
            return await Task.FromResult(0);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    // DEVNOTE ConfigurationBuilder is additive. Settings can be added and replaced with this process, but
                    // not removed. If you require a different shape for a particular configuration, it is best to
                    // remove a "default" from the base configuration, and specify in every environment, including Local
                    // https://www.paraesthesia.com/archive/2018/06/20/microsoft-extensions-configuration-deep-dive/
                    builder
                        .AddJsonFile("appsettings.json", true, false);
                    // Add configuration from an optional appsettings.development.json, appsettings.staging.json or
                    // appsettings.production.json file, depending on the environment. These settings override the ones in
                    // the appsettings.json file.
                    //.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, false)
                    // Add configuration specific to the Development, Staging or Production environments. This config can
                    // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
                    // override the ones in all of the above config files. See
                    // http://docs.asp.net/en/latest/security/app-secrets.html
                    //.AddEnvironmentVariables()
                    // Add command line options. These take the highest priority.
                    //.AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.AddDebug();
                        builder.AddConsole();
                    });

                    services.AddSingleton<ISessionDescriptorFactory, YamlSessionDescriptorFactory>();

                    services.AddSingleton<IActionBinder, ExpectActionBinder>();
                    services.AddSingleton<IActionBinder, ExpectUserActionBinder>();
                    services.AddSingleton<IActionBinder, SendActionBinder>();
                    services.AddSingleton<IActionBinder, SendUserActionBinder>();
                    services.AddSingleton<SessionRunner>();
                });
        }
    }
}