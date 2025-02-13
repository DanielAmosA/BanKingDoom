using Server.Helpers.WebEnum;
using Server.ModelsInterfaces.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for TransactionAction Entity 
    /// ( With extra data related to the Insert action )
    /// </summary>
    public class TransactionActionInsert : TransactionActionWithAPIResult , ITransactionActionInsert
    {
        public string Taz { get; set; }

        public TransactionActionInsert() : base()
        {
            Taz = string.Empty;
        }

    }
}
