using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace FiltersDemo.Filters
{


    public class RequestTimingFilter : IResourceFilter
    {
        private Stopwatch _watch;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _watch = Stopwatch.StartNew();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _watch.Stop();
            Console.WriteLine($"Request took {_watch.ElapsedMilliseconds} ms");
        }
    }
}
