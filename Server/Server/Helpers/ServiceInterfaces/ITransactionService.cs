using Server.Models.ExternalProvider;
using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Helpers.ServiceInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for TransactionService
    /// </summary>
    public interface ITransactionService
    {
        Task<ActionAPIResponse<string>> ExecuteTransactionAct<TTransactionAct>(TTransactionAct transactionAct, CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest)
        where TTransactionAct : ITransactionActionAPI;
        Task<ActionAPIResponse<string>> GetToken(CreateTokenRequest createTokenRequest);
    }
}
