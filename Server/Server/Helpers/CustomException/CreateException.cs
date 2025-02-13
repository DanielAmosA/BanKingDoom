namespace Server.Helpers.CustomException
{
    /// <summary>
    /// The class responsible for custom error for a situation 
    /// where the create failed.
    /// </summary>
    public class CreateException : Exception
    {
        public int StatusCode { get; set; }

        public CreateException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
