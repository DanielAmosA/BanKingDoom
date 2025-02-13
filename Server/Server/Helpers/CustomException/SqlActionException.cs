namespace Server.Helpers.CustomException
{
    /// <summary>
    /// The class responsible for custom error for a situation 
    /// where the Sql failed.
    /// </summary>

    public class SqlActionException : Exception
    {
        public int StatusCode { get; set; }

        public SqlActionException(string message) : base(message)
        {
            StatusCode = 401;
        }
    }
}
