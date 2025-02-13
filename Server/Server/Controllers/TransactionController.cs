using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Contracts;
using Server.Helpers.CustomException;
using Server.Helpers.Service;
using Server.Helpers.ServiceInterfaces;
using Server.Helpers.Validation;
using Server.Helpers.WebEnum;
using Server.Models.Entity;
using Server.Models.ExternalProvider;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;
using Server.Orchestration;
using Server.OrchestrationInterfaces;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.Controllers
{
    /// <summary>
    /// The Controller responsible for API actions for a Transaction
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase, ITransactionContracts
    {

        private readonly ITransactionOrcRead transactionOrcRead;
        private readonly ITransactionOrcWrite transactionOrcWrite;
        private readonly ITransactionHandlerService transactionHandlerService;
        private readonly IValidatorService<TransactionActionBasic> validatorTransactionActionBasic;
        private readonly IValidatorService<TransactionActionInsert> validatorTransactionActionInsert;

        public TransactionController(
            ITransactionOrcRead transactionOrcRead, ITransactionOrcWrite transactionOrcWrite,
            ITransactionHandlerService transactionHandlerService,
            IValidatorService<TransactionActionBasic> validatorTransactionActionBasic, IValidatorService<TransactionActionInsert> validatorTransactionActionInsert
            )
        {
            this.transactionHandlerService = transactionHandlerService;

            // Calling and executing services of the Orc (Orchestration).
            this.transactionOrcRead = transactionOrcRead;
            this.transactionOrcWrite = transactionOrcWrite;
            this.validatorTransactionActionBasic = validatorTransactionActionBasic;
            this.validatorTransactionActionInsert = validatorTransactionActionInsert;
        }

        [Authorize(Roles = "User")]
        [HttpDelete("TransactionActionDelete")]
        /// <summary>
        /// A service for deleting a transaction action by its ID.
        /// </summary>
        /// <param name="iTransactionActionID">The ID of the transaction action to delete.</param>
        /// <returns>
        ///     Returns the result of the deletion operation.
        ///     If the deletion fails or no data is found, returns a BadRequest with an error message.
        ///     If the deletion is successful, returns an Ok response with the relevant data.
        /// </returns>
        public async Task<IActionResult> TransactionActionDelete([FromQuery] int transactionActionID)
        {

            ResultSqlActionData<TransactionActionWithAPIResult> reusltTransactionActionDelete = await transactionOrcWrite.TransactionActionDelete(transactionActionID);
            if (reusltTransactionActionDelete.Data == null)
            {
                return BadRequest(new { error = reusltTransactionActionDelete.Error });
            }
            return Ok(reusltTransactionActionDelete.Data);

        }

        [Authorize(Roles = "User")]
        [HttpPost("TransactionActionInsert")]
        /// <summary>
        /// A service for inserting a transaction action.
        /// </summary>
        /// <param name="transactionActionInsert">The transaction action data to be inserted.</param>
        /// <returns>
        ///     Returns the inserted transaction data if successful.
        ///     If the model state is invalid, throws an exception.
        ///     If validation fails, returns a BadRequest with the validation errors.
        ///     If the insert operation fails, returns a BadRequest with the error details.
        /// </returns>
        public async Task<IActionResult> TransactionActionInsert([FromBody] TransactionActionInsert transactionActionInsert)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "The insert operation failed, one of the fields is incorrect." });
            }

            ValidationResultStruct result  = validatorTransactionActionInsert.Validate(transactionActionInsert);
            if (!result.isValid)
            {
                return BadRequest(new { errors = result.errors });
            }

            // After the data has been checked for correctness, we will initiate a call to a third - party service ( openBanking ) .

            CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest = new CreateDepositOrWithdrawalRequest
            {
               Amount = transactionActionInsert.Amount,
               Bank = transactionActionInsert.BankAccountNumber
            };
            string transactionType = transactionActionInsert.TransactionType;
            string taz = transactionActionInsert.Taz;
            ActionAPIResponse<string> resultTransactionActionAPI = await transactionHandlerService.HandleTransactionActionAPI(createDepositOrWithdrawalRequest, transactionType, taz);          
            if (resultTransactionActionAPI.Code != "200")
            {
                return BadRequest(new { error = resultTransactionActionAPI.Message });
            }
            else
            {
                transactionActionInsert.TokenResponse = resultTransactionActionAPI.Message!;
                transactionActionInsert.StatusAction = resultTransactionActionAPI.Data;
                // We saw that the operation was successful in adding to the DB.                
                ResultSqlActionData<TransactionActionWithAPIResult> reusltTransactionActionInsert = await transactionOrcWrite.TransactionActionInsert(transactionActionInsert);
                if (reusltTransactionActionInsert.Data == null)
                {
                    return BadRequest(new { error = reusltTransactionActionInsert.Error });
                }
                return Ok(reusltTransactionActionInsert.Data);
            }
            

        }

        [Authorize(Roles = "User")]
        [HttpPut("TransactionActionUpdate")]
        /// <summary>
        /// A service for updating a transaction action.
        /// </summary>
        /// <param name="transactionActionBasic">The transaction action data to update.</param>
        /// <param name="taz">The Register user ID.</param>
        /// <returns>
        ///     Returns the updated transaction data if successful.
        ///     If the model state is invalid, throws an exception.
        ///     If validation fails, returns a BadRequest with validation errors.
        ///     If the update operation fails, returns a BadRequest with an error message.
        /// </returns>
        public async Task<IActionResult> TransactionActionUpdate([FromQuery] string taz, [FromBody] TransactionActionBasic transactionActionBasic)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "The update operation failed, one of the fields is incorrect." });
            }

            ValidationResultStruct result = validatorTransactionActionBasic.Validate(transactionActionBasic);
            if (!result.isValid)
            {
                return BadRequest(new { errors = result.errors });
            }

            // After the data has been checked for correctness, we will initiate a call to a third - party service ( openBanking ) .

            CreateDepositOrWithdrawalRequest createDepositOrWithdrawalRequest = new CreateDepositOrWithdrawalRequest
            {
                Amount = transactionActionBasic.Amount,
                Bank = transactionActionBasic.BankAccountNumber
            };
            string transactionType = transactionActionBasic.TransactionType;
            ActionAPIResponse<string> resultTransactionActionAPI = await transactionHandlerService.HandleTransactionActionAPI(createDepositOrWithdrawalRequest, transactionType, taz);
            if (resultTransactionActionAPI.Code != "200")
            {
                return BadRequest(new { error = resultTransactionActionAPI.Message });
            }
            else
            {
                TransactionActionInsert transactionActionInsert = new TransactionActionInsert
                {
                    ID = transactionActionBasic.ID,
                    Amount = transactionActionBasic.Amount,
                    BankAccountNumber = transactionActionBasic.BankAccountNumber,
                    TokenResponse = resultTransactionActionAPI.Data,
                    StatusAction = resultTransactionActionAPI.Message!,
                };
            
                // We saw that the operation was successful in adding to the DB.
                ResultSqlActionData<TransactionActionBasic> reusltTransactionActionUpdate = await transactionOrcWrite.TransactionActionUpdate(transactionActionInsert);
                if (reusltTransactionActionUpdate.Data == null)
                {
                    return BadRequest(new { error = reusltTransactionActionUpdate.Error });
                }
                return Ok(reusltTransactionActionUpdate.Data);
            }

        }

        [Authorize(Roles = "User")]
        [HttpGet("TransactionHistoryGetTransactionHistoryByUserID")]
        /// <summary>
        /// A service for retrieving a user's transaction history.
        /// </summary>
        /// <param name="userID">The ID of the user whose transaction history is requested.</param>
        /// <returns>
        ///     Returns the transaction history of the user if found.
        ///     If no data is available, returns a BadRequest with an error message.
        /// </returns>
        public async Task<IActionResult> TransactionHistoryGetTransactionHistoryByUserID([FromQuery] int userID)
        {

            ResultSqlActionData<List<TransactionActionWithRegisterUserData>> reusltTransactionHistoryGetTransactionHistoryByUserID = 
                                                                    await transactionOrcRead.TransactionHistoryGetTransactionHistoryByUserID(userID);
            if (reusltTransactionHistoryGetTransactionHistoryByUserID.Data == null)
            {
                return NotFound(new { error = reusltTransactionHistoryGetTransactionHistoryByUserID.Error });
            }
            return Ok(reusltTransactionHistoryGetTransactionHistoryByUserID.Data);

        }
    }
}
