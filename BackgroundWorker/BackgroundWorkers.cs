using System.Collections.Generic;
using System.Linq;
using FluentScheduler;
using Serilog;

namespace BackgroundWorker
{
    public class BackgroundWorkers
    {
        private readonly IEnumerable<Registry> _registries;

        public BackgroundWorkers(IEnumerable<Registry> registries, ILogger logger)
        {
            _registries = registries;
            JobManager.JobStart += info => logger.Information($"Scheduled job {info.Name} started");
            JobManager.JobEnd += info => logger.Information($"Scheduled job {info.Name} finished");
            JobManager.JobException += info =>
                logger.Error(info.Exception, $"An error occurred on scheduled job {info.Name}");
        }

        public void Start()
        {
            JobManager.Initialize(_registries.ToArray());
        }

        public void Stop()
        {
            JobManager.Stop();
        }
    }
}
