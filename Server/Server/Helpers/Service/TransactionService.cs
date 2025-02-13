using Server.Helpers.ServiceInterfaces;
using Server.Models.ExternalProvider;
using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Helpers.Service
{
    /// <summary>
    /// The class responsible for receiving a token and actually executing the transaction.
    /// </summary>
    public class TransactionService: ITransactionService
    {
        private readonly IAPIClient apiClient;

        public TransactionService(IAPIClient apiClient)
        {
            this.apiClient = apiClient;
        }
       
        public async Task<ActionAPIResponse<string>> ExecuteTransactionAct<TTransactionAct>(TTransactionAct transactionAct, CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest)
               where TTransactionAct : ITransactionActionAPI
        {
            return await transactionAct.ExecuteTransactionActionAPI(createDepositOrWithdrawalRequest);
        }

        public async Task<ActionAPIResponse<string>> GetToken(CreateTokenRequest createTokenRequest)
        {
            var response = await apiClient.GoAPIClientAction<ActionAPIResponse<string>, CreateTokenRequest>(
        "createtoken",
        createTokenRequest,
        HttpMethod.Post);

            return new ActionAPIResponse<string>
            {
                Code = response.Code,
                Data = response.Data
            };
        }
    }
}
