using Microsoft.AspNetCore.Mvc;
using Server.Models.Entity;

namespace Server.Contracts
{
    /// <summary>
    /// The interface responsible for Contract management for TransactionController
    /// </summary>
    public interface ITransactionContracts
    {
        Task<IActionResult> TransactionActionDelete([FromQuery] int transactionActionID);

        Task<IActionResult> TransactionActionInsert([FromBody] TransactionActionInsert transactionActionInsert);

        Task<IActionResult> TransactionActionUpdate([FromQuery] string taz ,[FromBody] TransactionActionBasic transactionActionBasic);

        Task<IActionResult> TransactionHistoryGetTransactionHistoryByUserID([FromQuery] int userID);

    }
}
