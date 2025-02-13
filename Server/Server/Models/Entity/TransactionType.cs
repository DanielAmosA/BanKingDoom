using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Server.ModelsInterfaces.Entity;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for TransactionType Entity
    /// </summary>
    public class TransactionType : Basic , ITransactionType
    {
        public string Name { get; set; }

        public TransactionType()
        {
            Name = string.Empty;
        }
    }
}
