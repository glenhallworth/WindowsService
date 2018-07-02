using System;
using Autofac;
using AutofacSerilogIntegration;
using BackgroundWorker.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;
using Topshelf;

namespace BackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = GetStandardConfigurationBuilder();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();

            var appConfig = configuration.ParseAs<AppConfig>("AppConfig");
            
            var container = SetupContainer();

            var backgroundWorkers = container.Resolve<BackgroundWorkers>();

            var rc = HostFactory.Run(x =>                                  
            {
                x.UseSerilog();
                x.Service<BackgroundWorkers>(s =>                                   
                {
                    s.ConstructUsing(name => backgroundWorkers);                
                    s.WhenStarted(tc => tc.Start());                        
                    s.WhenStopped(tc => tc.Stop());                         
                });                                      

                x.SetDescription("Background Worker");                   
                x.SetDisplayName("Background Worker");                                  
                x.SetServiceName("Background Worker");                                 
            });                                                            

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  
            Environment.ExitCode = exitCode;
        }

        private static IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BackgroundWorkerModule());
            builder.RegisterLogger();
            return builder.Build();
        }

        internal static IConfiguration GetStandardConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
        }
    }
}
