using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDotNetCore
{
   public interface IProcessorAsyncJob // Async Job Processor Interface
    {
        public List<SortJob> PendingJobs { get; }
        public List<SortJob> CompletedJobs { get; }
        public void EnqueueJob(SortJob objJob);
        public List<SortJob> GetJobs();
        public SortJob? GetJob(Guid idGuid);
    }
}
