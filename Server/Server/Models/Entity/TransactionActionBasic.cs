using Newtonsoft.Json.Converters;
using Server.Helpers.WebEnum;
using Server.ModelsInterfaces.Entity;
using System.Text.Json.Serialization;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for TransactionActionBasic Entity (Basic Data)
    /// </summary>
    public class TransactionActionBasic : Basic, ITransactionActionBasic
    {
        public int Amount { get; set; }

        public string BankAccountNumber { get; set; }

        public string TransactionType { get; set; }

        public TransactionActionBasic() 
        {
            BankAccountNumber = string.Empty;
            TransactionType = string.Empty;
        }
    }
}
