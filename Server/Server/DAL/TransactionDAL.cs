using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Server.BL;
using Server.DALInterfaces;
using Server.Helpers.DB;
using Server.Helpers.DBInterfaces;
using Server.Helpers.General;
using Server.Helpers.Service;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;
using System.Data;

namespace Server.DAL
{
    /// <summary>
    /// The class responsible for Calling the procedures and their data 
    /// According to the Transaction area.
    /// </summary>
    public class TransactionDAL : ITransactionDAL
    {
        private readonly string connectionString;
        private readonly IDataHelper dataHelper;

        public TransactionDAL(IDataHelper dataHelper, IOptions<DbConfig> dbConfig)
        {
            //Get ConnectionString
            connectionString = dbConfig.Value.ConnectionString;
            // Calling and executing helper functions for SQL services.
            this.dataHelper = dataHelper;
        }

        public async Task<ResultSqlActionData<List<TransactionActionWithAPIResult>>> TransactionActionDelete(int iTransactionActionID)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@TransactionActionID", iTransactionActionID);
            DataTable? res =  await dataHelper.ExecSPWithRes(connectionString, SPNames.TRANSACTION_ACTION_DELETE, sqlParameters);
            return AppService.CheckRes<TransactionActionWithAPIResult>(res);
        }

        public async Task<ResultSqlActionData<List<TransactionActionWithAPIResult>>> TransactionActionInsert(TransactionActionInsert transactionActionInsert)
        {
            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@TazEncryption", transactionActionInsert.Taz);
            sqlParameters[1] = new SqlParameter("@Amount", transactionActionInsert.Amount);
            sqlParameters[2] = new SqlParameter("@BankAccountNumber", transactionActionInsert.BankAccountNumber);
            sqlParameters[3] = new SqlParameter("@TransactionType", transactionActionInsert.TransactionType);
            sqlParameters[4] = new SqlParameter("@StatusAction", transactionActionInsert.StatusAction);
            sqlParameters[5] = new SqlParameter("@TokenResponse", transactionActionInsert.TokenResponse);
            sqlParameters[6] = new SqlParameter("@CreatedAt", transactionActionInsert.CreatedAt);
            DataTable? res = await dataHelper.ExecSPWithRes(connectionString, SPNames.TRANSACTION_ACTION_INSERT, sqlParameters);
            return AppService.CheckRes<TransactionActionWithAPIResult>(res);
        }

        public async Task<ResultSqlActionData<List<TransactionActionBasic>>> TransactionActionUpdate(TransactionActionInsert transactionActionInsert)
        {
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@TransactionActionID", transactionActionInsert.ID);
            sqlParameters[1] = new SqlParameter("@NewAmount", transactionActionInsert.Amount);
            sqlParameters[2] = new SqlParameter("@NewBankAccountNumber", transactionActionInsert.BankAccountNumber);
            sqlParameters[3] = new SqlParameter("@NewTokenResponse", transactionActionInsert.StatusAction);
            sqlParameters[4] = new SqlParameter("@NewStatusAction", transactionActionInsert.TokenResponse);
            sqlParameters[5] = new SqlParameter("@NewUpdateAt", transactionActionInsert.UpdatedAt);
            DataTable? res = await dataHelper.ExecSPWithRes(connectionString, SPNames.TRANSACTION_ACTION_UPDATE, sqlParameters);
            return AppService.CheckRes<TransactionActionBasic>(res);
        }

        public async Task<ResultSqlActionData<List<TransactionActionWithRegisterUserData>>> TransactionHistoryGetTransactionHistoryByUserID(int userID)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserID", userID);
            DataTable? res = await dataHelper.ExecSPWithRes(connectionString, SPNames.TRANSACTIONHISTORY_GETTRANSACTIONHISTORYBYUSERID, sqlParameters);
            return AppService.CheckRes<TransactionActionWithRegisterUserData>(res);
        }

    }
}
