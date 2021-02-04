using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDotNetCore
{
    public class SortJob
    {
        public SortJob(Guid id, SortStatusJob status, TimeSpan? duration, IReadOnlyCollection<int> input, IReadOnlyCollection<int>? output)
        {
            Id = id;
            Status = status;
            Duration = duration;
            Input = input;
            Output = output;
        }

        //Properties to Create
        public Guid Id { get; }
        public SortStatusJob Status { get; }
        public TimeSpan? Duration { get; }
        public IReadOnlyCollection<int> Input { get; }
        public IReadOnlyCollection<int>? Output { get; }
    }
}
