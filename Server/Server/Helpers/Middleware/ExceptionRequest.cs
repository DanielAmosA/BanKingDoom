using Azure.Core;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Serilog;
using Server.Helpers.CustomException;
using Server.Helpers.MiddlewareInterfaces;

namespace Server.Helpers.Middleware
{
    /// <summary>
    /// The class responsible for Tracking errors while executing HTTP requests.
    /// </summary>
    public class ExceptionRequest : IExceptionRequest
    {
        private readonly RequestDelegate next;

        public ExceptionRequest(RequestDelegate next)
        {
            // Tracking errors.
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            //This function unifies all the error handling logic.
            //It uses a switch to determine the status code for each type of error.
            var statusCode = ex switch
            {
                ConflictException conflictEx => conflictEx.StatusCode,
                CreateException createEx => createEx.StatusCode,
                DeleteException deleteEx => deleteEx.StatusCode,
                FieldsException FieldsEx => FieldsEx.StatusCode,
                ForbiddenException ForbiddenEx => ForbiddenEx.StatusCode,
                NotFoundException NotFoundEx => NotFoundEx.StatusCode,
                SqlActionException SqlActionEx => SqlActionEx.StatusCode,
                UpdateException updateEx => updateEx.StatusCode,                
                _ => 500 // Server error for other types
            };

            var errorResponse = new { message = ex.Message };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            Log.Error($"Error: {statusCode} =>  {errorResponse}");

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
