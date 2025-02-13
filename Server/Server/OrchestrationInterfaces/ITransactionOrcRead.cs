using Server.Models.Entity;
using Server.Models.Settings;

namespace Server.OrchestrationInterfaces
{

    /// <summary>
    /// The interface responsible for Structure declaration for TransactionOrcRead
    /// </summary>
    public interface ITransactionOrcRead
    {
        Task<ResultSqlActionData<List<TransactionActionWithRegisterUserData>>> TransactionHistoryGetTransactionHistoryByUserID(int userID);
    }
}
