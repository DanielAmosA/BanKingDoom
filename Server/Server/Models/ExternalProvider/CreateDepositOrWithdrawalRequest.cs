using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Models.ExternalProvider
{
    /// <summary>
    /// The class responsible for Structure declaration for CreateDeposit / CreateWithdrawal 
    /// ( External provider - OpenBanking )  request parameters.
    /// </summary>

    public class CreateDepositOrWithdrawalRequest : IActionRequest, IBankAccountActionRequest
    {
        public int Amount { get; set; }
        public string Bank { get; set; }
        public string Token { get; set; }

        public CreateDepositOrWithdrawalRequest()
        {
            Bank = string.Empty;
            Token = string.Empty;
        }
    }
}
