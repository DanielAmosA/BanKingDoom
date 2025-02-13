namespace Server.Helpers.MiddlewareInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for LogRequest
    /// </summary>
    public interface ILogRequest
    {
        Task InvokeAsync(HttpContext httpContext);
    }
}
