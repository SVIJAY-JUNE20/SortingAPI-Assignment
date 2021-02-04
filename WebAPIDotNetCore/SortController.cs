using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDotNetCore
{
    [ApiController]  
    [Route("Sort")]

    public class SortController : ControllerBase
    {
        private readonly IProcessorSort _sortJobProcessor;
        private readonly IProcessorAsyncJob _sortAsyncJob;

        public SortController(IProcessorSort Sort1, IProcessorAsyncJob Async1)
        {
            _sortJobProcessor = Sort1;
            _sortAsyncJob = Async1;
        }

        [HttpPost("Run")]
        [Obsolete("This executes the sort job asynchronously. Use the asynchronous 'EnqueueJob' instead.")]
        public async Task<ActionResult<SortJob>> EnqueueAndRunJob(int[] values)
        {
            var pendingJob = new SortJob(
                id: Guid.NewGuid(),
                status: SortStatusJob.Pending,
                duration: null,
                input: values,
                output: null);

            var completedJob = await _sortJobProcessor.Process(pendingJob);

            return Ok(completedJob);
        }

       
        [HttpPost]
        public ActionResult<SortJob> EnqueueJob([FromForm] int[] values)
        {
            var pendingJob = new SortJob(
                id: Guid.NewGuid(),
                status: SortStatusJob.Pending,
                duration: null,
                input: values,
                output: null);

            _sortAsyncJob.EnqueueJob(pendingJob);
            return Ok(pendingJob);
        }

        [HttpGet]
        public ActionResult<SortJob[]> GetJobs()
        {
            return _sortAsyncJob.GetJobs().ToArray();

        }

        [HttpGet("{jobId}")]
        public ActionResult<SortJob> GetJob([FromRoute] Guid jobId)
        {
            SortJob? sortJob = _sortAsyncJob.GetJob(jobId);

            if (sortJob == null)
                return NotFound();

            return sortJob;
        }
        //[HttpPost]
        //public Task<ActionResult<SortJob>> EnqueueJob(int[] values)
        //{
        //    // TODO: Should enqueue a job to be processed in the background.
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //public Task<ActionResult<SortJob[]>> GetJobs()
        //{
        //    // TODO: Should return all jobs that have been enqueued (both pending and completed).
        //    throw new NotImplementedException();
        //}

        //[HttpGet("{jobId}")]
        //public Task<ActionResult<SortJob>> GetJob(Guid jobId)
        //{
        //    // TODO: Should return a specific job by ID.
        //    throw new NotImplementedException();
        //}
    }
}
