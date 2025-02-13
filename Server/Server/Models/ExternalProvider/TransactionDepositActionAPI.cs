using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Models.ExternalProvider
{
    /// <summary>
    /// The class is responsible for being used as a call to create a createdeposit API action
    /// </summary>
    public class TransactionDepositActionAPI : ITransactionDepositActionAPI, ITransactionActionAPI
    {
        public string TransactionActionAPIUrl => "createdeposit";

        public async Task<ActionAPIResponse<string>> ExecuteTransactionActionAPI(CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest)
        {
            // Actual implementation would go here
            // This is a mock implementation
            return new ActionAPIResponse<string>
            {
                Code = "200",
                Data = "Deposit_Completed",
                Message = createDepositOrWithdrawalRequest.Token
                
            };
        }
    }
}
