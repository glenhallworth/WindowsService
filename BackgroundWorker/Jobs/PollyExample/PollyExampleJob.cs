using System;
using System.Threading.Tasks;
using FluentScheduler;
using Polly;
using Serilog;

namespace BackgroundWorker.Jobs.PollyExample
{
    public class PollyExampleJob : IJob
    {
        private readonly ILogger _logger;

        public PollyExampleJob(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute()
        {
            ExecuteAsync().Wait();
        }

        private async Task ExecuteAsync()
        {
            var policyResult = await Policy
                .Handle<Exception>()
                .RetryAsync(3)
                .ExecuteAndCaptureAsync(async () =>
                {
                    _logger.Information("Attempting to retrieve");
                    return await DoThing();
                });

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                _logger.Error(policyResult.FinalException, "Could not retrieve status");
                return;
            }

            var response = policyResult.Result;
            _logger.Information($"Status is {response}");
        }

        private Task<bool> DoThing()
        {
            var rnd = new Random();
            if (rnd.Next(1, 100) > 50)
            {
                throw new Exception("whoops");
            }

            return Task.FromResult(true);
        }
    }
}