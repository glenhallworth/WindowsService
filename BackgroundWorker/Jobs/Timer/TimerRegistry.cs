using BackgroundWorker.Infrastructure;
using FluentScheduler;

namespace BackgroundWorker.Jobs.Timer
{
    class TimerRegistry : Registry
    {
        public TimerRegistry(TimerJob job, AppConfig appConfig)
        {
            if (appConfig.RunTimerJob)
            {
                Schedule(job).WithName(job.GetType().FullName).ToRunNow();
            }
        }
    }
}
