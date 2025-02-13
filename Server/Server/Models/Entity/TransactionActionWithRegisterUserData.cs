using Server.ModelsInterfaces.Entity;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for TransactionAction Entity 
    /// ( With extra data related to the RegisterUser entity )
    /// </summary>
    public class TransactionActionWithRegisterUserData : TransactionActionWithAPIResult, ITransactionActionWithRegisterUserData
    {       
        public string HebrewFullName { get; set; }

        public string EnglishFullName { get; set; }

        public TransactionActionWithRegisterUserData() :base()
        {
            HebrewFullName = string.Empty;
            EnglishFullName = string.Empty;
        }
    }
}
