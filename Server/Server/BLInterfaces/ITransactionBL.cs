using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;

namespace Server.BLInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for TransactionBL
    /// </summary>
    public interface ITransactionBL : IDisposable
    {
        Task<ResultSqlActionData<TransactionActionWithAPIResult>> TransactionActionDelete(int iTransactionActionID);
        Task<ResultSqlActionData<TransactionActionWithAPIResult>> TransactionActionInsert(TransactionActionInsert transactionActionInsert);
        Task<ResultSqlActionData<TransactionActionBasic>> TransactionActionUpdate(TransactionActionInsert transactionActionInsert);
        Task<ResultSqlActionData<List<TransactionActionWithRegisterUserData>>> TransactionHistoryGetTransactionHistoryByUserID(int userID);
    }
}
