using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Contracts;
using Server.Helpers.CustomException;
using Server.Helpers.ServiceInterfaces;
using Server.Helpers.Validation;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.Orchestration;
using Server.OrchestrationInterfaces;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.Controllers
{
    /// <summary>
    /// The Controller responsible for API actions for a Register User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase , IRegisterUserContracts
    {

        private readonly IRegisterUserOrcRead registerUserOrcRead;
        private readonly IRegisterUserOrcWrite registerUserOrcWrite;
        private readonly IValidatorService<RegisterUser> validatorRegisterUser;
        private readonly IValidatorService<RegisterUserBasic> validatorRegisterUserBasic;
        public RegisterUserController(IRegisterUserOrcRead registerUserOrcRead, IRegisterUserOrcWrite registerUserOrcWrite,
                                      IValidatorService<RegisterUser> validatorRegisterUser, IValidatorService<RegisterUserBasic> validatorRegisterUserBasic)
        {
            // Calling and executing services of the Orc (Orchestration).
            this.registerUserOrcRead = registerUserOrcRead;
            this.registerUserOrcWrite = registerUserOrcWrite;
            this.validatorRegisterUser = validatorRegisterUser;
            this.validatorRegisterUserBasic = validatorRegisterUserBasic;
        }

        [HttpPost("RegisterUserInsert")]
        /// <summary>
        /// A service for inserting a new user.
        /// </summary>
        /// <param name="registerUser">The user data to be inserted.</param>
        /// <returns>
        ///     Returns the inserted user data if successful.
        ///     If the model state is invalid, throws an exception.
        ///     If validation fails, returns a BadRequest with validation errors.
        ///     If the insert operation fails, throws an exception with the error details.
        /// </returns>    
        public async Task<IActionResult> RegisterUserInsert([FromBody] RegisterUser registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "The insert operation failed, one of the fields is incorrect." });
            }

            ValidationResultStruct result = validatorRegisterUser.Validate(registerUser);
            if (!result.isValid)
            {
                return BadRequest( new { errors = result.errors } );
            }

            ResultSqlActionData<RegisterUser> reusltRegisterUserInsert = await registerUserOrcWrite.RegisterUserInsert(registerUser);
            if (reusltRegisterUserInsert.Data == null)
            {
                return BadRequest(new { error = reusltRegisterUserInsert.Error });
            }
            return Ok(reusltRegisterUserInsert.Data);

        }

        [HttpPost("RegisterUserGetUserByTaz")]
        /// <summary>
        /// A service for retrieving a registered user by their Taz.
        /// </summary>
        /// <param name="registerUserBasic">The user data containing the TAZ to search for.</param>
        /// <returns>
        ///     Returns the user data if found.
        ///     If the model state is invalid, throws an exception.
        ///     If validation fails, returns a BadRequest with validation errors.
        ///     If the user is not found, returns a BadRequest with an error message.
        /// </returns>
        public async Task<IActionResult> RegisterUserGetUserByTaz([FromBody] RegisterUserBasic registerUserBasic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {error =  "The get operation failed, one of the fields is incorrect." });
            }

            ValidationResultStruct validationResultStruct = validatorRegisterUserBasic.Validate(registerUserBasic);
            if (!validationResultStruct.isValid)
            {
                return BadRequest(new { errors = validationResultStruct.errors });
            }

            ResultSqlActionData<RegisterUser> reusltRegisterUserGetUserByTaz = await registerUserOrcRead.RegisterUserGetUserByTaz(registerUserBasic);
            if (reusltRegisterUserGetUserByTaz.Data == null)
            {
                return NotFound(new { error = reusltRegisterUserGetUserByTaz.Error });
            }
            return Ok(reusltRegisterUserGetUserByTaz.Data);

        }

    }
}
