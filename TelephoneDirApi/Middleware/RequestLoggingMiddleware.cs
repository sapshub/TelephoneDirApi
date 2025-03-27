using System.Diagnostics;
using System.Text;

namespace TelephoneDirApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var stopwatch = Stopwatch.StartNew();

            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            _logger.LogInformation($"Request: {request.Method} {request.Path} | Body: {body}");

            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation($"Response: {context.Response.StatusCode} | Time Taken: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
