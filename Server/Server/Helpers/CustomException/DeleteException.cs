namespace Server.Helpers.CustomException
{
    /// <summary>
    /// The class responsible for custom error for a situation 
    /// where the Delete failed.
    /// </summary>

    public class DeleteException : Exception
    {
        public int StatusCode { get; set; }

        public DeleteException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
