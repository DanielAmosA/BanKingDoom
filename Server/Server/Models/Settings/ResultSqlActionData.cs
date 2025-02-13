using Server.ModelsInterfaces.Settings;

namespace Server.Models.Settings
{

    /// <summary>
    /// The class responsible for Structure declaration for the result of an SQL action, 
    /// which can be success (with data) or failure (with an error message).
    /// </summary>
    public class ResultSqlActionData<TModel> 
                                                             where TModel : class
    {
        public TModel? Data { get; set; }
        public string? Error { get; set; }
        public bool IsSuccess => Data == null;

        public static ResultSqlActionData<TModel> InSuccess(TModel resultSqlActionData) => 
                                                  new ResultSqlActionData<TModel> { Data = resultSqlActionData };
        public static ResultSqlActionData<TModel> InError(string error) => 
                                                 new ResultSqlActionData<TModel> { Error = error };
    }
}
