using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDotNetCore
{
   public interface IProcessorSort // Sorting the Job Processor Interface.
    {
        Task<SortJob> Process(SortJob job);
    }
}
