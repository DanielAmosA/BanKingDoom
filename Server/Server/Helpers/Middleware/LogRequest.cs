using Serilog;
using Server.Helpers.MiddlewareInterfaces;

namespace Server.Helpers.Middleware
{
    /// <summary>
    /// The class responsible for Tracking every request sent to the server.
    /// </summary>
    public class LogRequest :ILogRequest
    {
        private readonly RequestDelegate next;

        public LogRequest(RequestDelegate next)
        {
            // Go tracking of the HTTP.
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var request = httpContext.Request;

            // Info Log
            Log.Information($"Incoming request: {request.Method} {request.Path}");

            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                //Error Log
                Log.Error(ex, "An error occurred while processing the request");
                throw;
            }
        }
    }
}
