using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;

namespace Server.DALInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for TransactionDAL
    /// </summary>
    public interface ITransactionDAL
    {
        Task<ResultSqlActionData<List<TransactionActionWithAPIResult>>> TransactionActionDelete(int iTransactionActionID);
        
        Task<ResultSqlActionData<List<TransactionActionWithAPIResult>>> TransactionActionInsert(TransactionActionInsert transactionActionInsert);
        
        Task<ResultSqlActionData<List<TransactionActionBasic>>> TransactionActionUpdate(TransactionActionInsert transactionActionInsert);
        
        Task<ResultSqlActionData<List<TransactionActionWithRegisterUserData>>> TransactionHistoryGetTransactionHistoryByUserID(int userID);
    }
}
