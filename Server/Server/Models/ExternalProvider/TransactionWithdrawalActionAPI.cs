using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Models.ExternalProvider
{
    /// <summary>
    /// The class is responsible for being used as a call to create a createWithdrawal API action
    /// </summary>
    public class TransactionWithdrawalActionAPI : ITransactionDepositActionAPI, ITransactionActionAPI
    {
        public string TransactionActionAPIUrl => "createWithdrawal";

        public async Task<ActionAPIResponse<string>> ExecuteTransactionActionAPI(CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest)
        {
            // Actual implementation would go here
            // This is a mock implementation
            return new ActionAPIResponse<string>
            {
                Code = "200",
                Data = "Withdrawal_Completed",
                Message = createDepositOrWithdrawalRequest.Token
            };
        }
    }
}
