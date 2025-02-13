using Server.Helpers.WebEnum;
using Server.ModelsInterfaces.Entity;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for TransactionAction Entity 
    /// ( With extra data related to the API ( openBanking ) result )
    /// </summary>
    public class TransactionActionWithAPIResult : TransactionActionBasic , ITransactionActionWithAPIResult
    {

        public string StatusAction { get; set; }

        public string TokenResponse { get; set; }

        public TransactionActionWithAPIResult() : base()
        {
            StatusAction = string.Empty;
            TokenResponse = string.Empty;
        }
    }
}
