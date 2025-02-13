using Azure;
using Microsoft.Extensions.Options;
using Serilog.Filters;
using Server.Helpers.CustomException;
using Server.Helpers.ServiceInterfaces;
using Server.Models.ExternalProvider;
using Server.Models.Settings;
using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Helpers.Service
{

    /// <summary>
    /// The class responsible for managing the process of initiating a transaction.
    //  It includes fetching a token, checking the type of operation, and executing the operation.
    /// </summary>
    public class TransactionHandlerService : ITransactionHandlerService
    {
        private readonly ITransactionService transactionService;
        private readonly IDictionary<string, ITransactionActionAPI> transactionActionsAPI;
        private readonly string secretId;

        public TransactionHandlerService(
            ITransactionService transactionService,
            IEnumerable<ITransactionActionAPI> transactionActionsAPI,
            IOptions<OpenBankingParameters> openBankingParameters)
        {
            this.transactionService = transactionService;
            this.transactionActionsAPI = new Dictionary<string, ITransactionActionAPI>
            {
                { "Deposit", new TransactionDepositActionAPI() },
                { "Withdrawal", new TransactionWithdrawalActionAPI() }
            };
            this.secretId = openBankingParameters.Value.SecretId;
        }

        public async Task<ActionAPIResponse<string>> HandleTransactionActionAPI(CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest, string transactionType, string userID)
        {
            try
            {
                // Get token
                var tokenResponse = await transactionService.GetToken(new CreateTokenRequest
                {
                    UserID = userID,
                    SecretID = this.secretId
                });

                // Verify that the response code is success ( 200 ).
                if (tokenResponse.Code != "200")
                {
                    return new ActionAPIResponse<string>
                    {
                        Code = tokenResponse.Code,
                        Message = "Token creation failed"
                    };
                }

                // Add token to request
                createDepositOrWithdrawalRequest.Token = tokenResponse.Data;

                // Get Transaction Action type
                if (!transactionActionsAPI.TryGetValue(transactionType, out var action))
                {
                    return new ActionAPIResponse<string>
                    {
                        Code = "400",
                        Message = $"Unknown transaction type: {transactionType}"
                    };
                }

                // Execute Transaction Action
                ActionAPIResponse<string> resHandleTransactionActionAPI = await transactionService.ExecuteTransactionAct(action, createDepositOrWithdrawalRequest);
                
                if(resHandleTransactionActionAPI.Code == "200")
                {
                    // Save Token
                    resHandleTransactionActionAPI.Message = createDepositOrWithdrawalRequest.Token;
                }
                return resHandleTransactionActionAPI;
            }
            catch (Exception ex)
            {
                throw new APIActionException($"API call exception occurred : {ex.Message}");
            }
        }
    }
}
