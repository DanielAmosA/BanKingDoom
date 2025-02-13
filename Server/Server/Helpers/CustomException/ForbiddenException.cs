namespace Server.Helpers.CustomException
{
    /// <summary>
    /// The class responsible for custom error for a situation 
    /// where the User is not authorized to action.
    /// </summary>

    public class ForbiddenException : Exception
    {
        public int StatusCode { get; set; }

        public ForbiddenException(string message) : base(message)
        {
            StatusCode = 403;
        }
    }
}
