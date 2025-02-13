using Server.ModelsInterfaces.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for TransactionHistory Entity
    /// </summary>
    public class TransactionHistory : ITransactionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("TransactionActionID")]
        public int TransactionActionID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }

    }
}
