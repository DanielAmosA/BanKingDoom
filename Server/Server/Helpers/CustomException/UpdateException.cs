namespace Server.Helpers.CustomException
{
    /// <summary>
    /// The class responsible for custom error for a situation 
    /// where a situation where the Update failed.
    /// </summary>

    public class UpdateException : Exception
    {
        public int StatusCode { get; set; }

        public UpdateException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
