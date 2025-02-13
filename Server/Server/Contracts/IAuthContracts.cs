using Microsoft.AspNetCore.Mvc;
using Server.Helpers.WebEnum;

namespace Server.Contracts
{
    /// <summary>
    /// The interface responsible for Contract management for AuthController
    /// </summary>
    public interface IAuthContracts
    {
        IActionResult GenerateToken(Roles role);
    }
}
