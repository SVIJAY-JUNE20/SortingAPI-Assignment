using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDotNetCore
{
    public class SortJobProcessorWithoutAsync : IProcessorSort
    {
        private readonly ILogger<SortJobProcessorWithoutAsync> _logger;

        public SortJobProcessorWithoutAsync(ILogger<SortJobProcessorWithoutAsync> logger)
        {
            _logger = logger;
        }
        public async Task<SortJob> Process(SortJob job)
        {
            _logger.LogInformation("Processing job with ID '{JobId}'.", job.Id);

            var stopwatch = Stopwatch.StartNew();

            var output = job.Input.OrderBy(n => n).ToArray();
            await Task.Delay(50000); // NOTE: This is just to simulate a more expensive operation

            var duration = stopwatch.Elapsed;

            _logger.LogInformation("Completed processing job with ID '{JobId}'. Duration: '{Duration}'.", job.Id, duration);

            return new SortJob(
                id: job.Id,
                status: SortStatusJob.Completed,
                duration: duration,
                input: job.Input,
                output: output);
        }
    }
    
}
