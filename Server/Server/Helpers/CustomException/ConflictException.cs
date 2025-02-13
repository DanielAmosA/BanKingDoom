namespace Server.Helpers.CustomException
{
    /// <summary>
    /// The class responsible for custom error for a situation 
    /// where the action failed
    /// because of dependent relationships.
    /// </summary>
    public class ConflictException : Exception
    {
        public int StatusCode { get; set; }

        public ConflictException(string message) : base(message)
        {
            StatusCode = 409;
        }
    }
}
