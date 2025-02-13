using Server.BLInterfaces;
using Server.DAL;
using Server.DALInterfaces;
using Server.Helpers.Service;
using Server.Helpers.ServiceInterfaces;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;
using Server.ModelsInterfaces.Settings;
using System.Collections.Generic;

namespace Server.BL
{
    /// <summary>
    /// The class responsible for Transaction Business Logic 
    /// Before being sent to the TransactionDAL (Data Access Layer).
    /// </summary>
    public class TransactionBL : ITransactionBL
    {
        private readonly ITransactionDAL transactionDAL;
        private readonly ISecurityService securityService;

        public TransactionBL(ITransactionDAL transactionDAL, ISecurityService securityService)
        {
            this.transactionDAL = transactionDAL;
            this.securityService = securityService;
        }

        public async Task<ResultSqlActionData<TransactionActionWithAPIResult>> TransactionActionDelete(int transactionActionID)
        {
            ResultSqlActionData<List<TransactionActionWithAPIResult>> resTransactionActionDelete = 
                                                                      await transactionDAL.TransactionActionDelete(transactionActionID);
            return AppService.ProcessResGetFirstRow(resTransactionActionDelete);
        }

        public async Task<ResultSqlActionData<TransactionActionWithAPIResult>> TransactionActionInsert(TransactionActionInsert transactionActionInsert)
        {
            transactionActionInsert.CreatedAt = transactionActionInsert.CreatedAt == default
                ? DateTime.Now
                : transactionActionInsert.CreatedAt;

            transactionActionInsert.Taz = securityService.CreateEncryptorValue(transactionActionInsert.Taz);
            ResultSqlActionData<List<TransactionActionWithAPIResult>> resTransactionActionInsert = 
                                                                      await transactionDAL.TransactionActionInsert(transactionActionInsert);
            return AppService.ProcessResGetFirstRow(resTransactionActionInsert);
        }

        public async Task<ResultSqlActionData<TransactionActionBasic>> TransactionActionUpdate(TransactionActionInsert transactionActionInsert)
        {
            ResultSqlActionData<List<TransactionActionBasic>> resTransactionActionUpdate = 
                                                              await transactionDAL.TransactionActionUpdate(transactionActionInsert);
            return AppService.ProcessResGetFirstRow(resTransactionActionUpdate);
        }

        public async Task<ResultSqlActionData<List<TransactionActionWithRegisterUserData>>> TransactionHistoryGetTransactionHistoryByUserID(int userID)
        {

            return await transactionDAL.TransactionHistoryGetTransactionHistoryByUserID(userID);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
