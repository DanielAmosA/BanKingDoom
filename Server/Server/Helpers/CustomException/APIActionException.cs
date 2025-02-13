namespace Server.Helpers.CustomException
{
    public class APIActionException : Exception
    {
        public int StatusCode { get; set; }

        public APIActionException(string message) : base(message)
        {
            StatusCode = 503;
        }
    }
}
