using Server.BL;
using Server.BL.Factory;
using Server.BLInterfaces;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;
using Server.ModelsInterfaces.Settings;
using Server.OrchestrationInterfaces;

namespace Server.Orchestration
{
    /// <summary>
    /// The class responsible for Managing Read requests 
    /// From the TransactionController (API) to the TransactionBL (Business Logic).
    /// With using the Factory Design Pattern
    /// </summary>
    public class TransactionOrcRead : ITransactionOrcRead
    {
        private readonly IFactory<TransactionBL> transactionBLFactory;

        public TransactionOrcRead(IFactory<TransactionBL> transactionBLFactory)
        {
            // Inject the transactionBLFactory,
            // which is a factorization that will provide TransactionBL objects.
            // This factory creates the Business Logic(BL) objects.
            this.transactionBLFactory = transactionBLFactory;
        }

        public async Task<ResultSqlActionData<List<TransactionActionWithRegisterUserData>>> TransactionHistoryGetTransactionHistoryByUserID(int userID)
        {
            using (TransactionBL transactionBL = transactionBLFactory.Create())
            {
                return await transactionBL.TransactionHistoryGetTransactionHistoryByUserID(userID);
            }
        }
    }
}
