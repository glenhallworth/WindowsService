using BackgroundWorker.Infrastructure;
using FluentScheduler;

namespace BackgroundWorker.Jobs.PollyExample
{
    class PollyExampleRegsitry : Registry
    {
        public PollyExampleRegsitry(PollyExampleJob job, AppConfig appConfig)
        {
            Schedule(job).WithName(job.GetType().FullName).ToRunNow();
        }
    }
}
