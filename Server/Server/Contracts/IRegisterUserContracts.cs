using Microsoft.AspNetCore.Mvc;
using Server.Models.Entity;

namespace Server.Contracts
{
    /// <summary>
    /// The interface responsible for Contract management for RegisterUserController
    /// </summary>
    public interface IRegisterUserContracts
    {
        Task<IActionResult> RegisterUserInsert([FromBody] RegisterUser registerUser);
        Task<IActionResult> RegisterUserGetUserByTaz([FromBody] RegisterUserBasic registerUserBasic);
    }
}
