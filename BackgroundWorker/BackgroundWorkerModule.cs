using Autofac;
using BackgroundWorker.Infrastructure;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace BackgroundWorker
{
    public class BackgroundWorkerModule : Module
    {
        public BackgroundWorkerModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(TheBackgroundWorker.Assembly)
                .Where(t => t.IsAssignableTo<IJob>()).AsSelf();

            builder.RegisterAssemblyTypes(TheBackgroundWorker.Assembly)
                .Where(t => t.IsSubclassOf(typeof(Registry))).As<Registry>();

            builder.RegisterType<BackgroundWorkers>();
            
            builder.RegisterType<ConfigurationBuilder>();
            builder.Register(context =>
            {
                var configBuilder = context.Resolve<ConfigurationBuilder>();
                return configBuilder
                    .AddJsonFile("appsettings.json").Build().ParseAs<AppConfig>("AppConfig");
            });
        }
    }
}