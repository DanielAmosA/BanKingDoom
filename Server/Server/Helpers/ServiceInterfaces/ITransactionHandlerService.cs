using Server.Models.ExternalProvider;
using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Helpers.ServiceInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for TransactionHandlerService
    /// </summary>
    public interface ITransactionHandlerService
    {
        Task<ActionAPIResponse<string>> HandleTransactionActionAPI(CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest, string transactionType, string userID);
    }
}
