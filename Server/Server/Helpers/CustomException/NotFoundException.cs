namespace Server.Helpers.CustomException
{
    /// <summary>
    /// The class responsible for custom error for a situation 
    /// where No resources are found.
    /// </summary>

    public class NotFoundException : Exception
    {
        public int StatusCode { get; set; }

        public NotFoundException(string message) : base(message)
        {
            StatusCode = 404;
        }
    }
}
