using System;
using FluentScheduler;

namespace BackgroundWorker.Jobs.Timer
{
    public class TimerJob : IJob
    {

        public void Execute()
        {
            var timer = new System.Timers.Timer(1000) { AutoReset = true };
            timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
            timer.Start();;
        }
    }
}