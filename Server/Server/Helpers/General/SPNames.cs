namespace Server.Helpers.General
{
    /// <summary>
    /// The class responsible for represents the names of stored procedures (SP) System.
    /// </summary>
    public static class SPNames
    {
        #region SP - User 

        public const string REGISTER_USER_INSERT = "RegisterUser_Insert";
        public const string REGISTER_USER_GET_USER_BY_TAZ = "RegisterUser_GetUserByTaz";

        #endregion

        #region SP - Transaction 

        public const string TRANSACTION_ACTION_DELETE = "TransactionAction_Delete";
        public const string TRANSACTION_ACTION_INSERT = "TransactionAction_Insert";
        public const string TRANSACTION_ACTION_UPDATE = "TransactionAction_Update";
        public const string TRANSACTIONHISTORY_GETTRANSACTIONHISTORYBYUSERID = "TransactionHistory_GetTransactionHistoryByUserID";
        #endregion
    }
}
