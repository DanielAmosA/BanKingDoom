using Server.Models.ExternalProvider;

namespace Server.ModelsInterfaces.ExternalProvider
{
    /// <summary>
    /// The interface responsible for Structure declaration for Basic TransactionActionAPI
    /// </summary>
    public interface ITransactionActionAPI
    {
        Task<ActionAPIResponse<string>> ExecuteTransactionActionAPI(CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest);
        string TransactionActionAPIUrl { get; }
    }
}
