using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;

namespace Server.OrchestrationInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for TransactionOrcWrite
    /// </summary>
    public interface ITransactionOrcWrite
    {
        Task<ResultSqlActionData<TransactionActionWithAPIResult>> TransactionActionDelete(int iTransactionActionID);
        Task<ResultSqlActionData<TransactionActionWithAPIResult>> TransactionActionInsert(TransactionActionInsert transactionActionInsert);
        Task<ResultSqlActionData<TransactionActionBasic>> TransactionActionUpdate(TransactionActionInsert transactionActionInsert);
    }
}
