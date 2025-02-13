using Server.Models.Settings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.ModelsInterfaces.Settings
{
    /// <summary>
    /// The interface responsible for Structure declaration for ResultSqlActionData
    /// </summary>
    public interface IResultSqlActionData<TResultSqlActionData> where TResultSqlActionData : class
    {
        ResultSqlActionData<TResultSqlActionData> InSuccess(TResultSqlActionData resultSqlActionData);
        ResultSqlActionData<TResultSqlActionData> InError(string error);
    }
}
