using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TelephoneDirApi.Filter
{
    public class ExecutionTimeFilter : IActionFilter
    {
        private readonly ILogger<ExecutionTimeFilter> _logger;
        private Stopwatch _stopwatch;

        public ExecutionTimeFilter(ILogger<ExecutionTimeFilter> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} executed in {_stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
