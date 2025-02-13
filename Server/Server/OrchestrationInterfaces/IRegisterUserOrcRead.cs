using Server.Models.Entity;
using Server.Models.Settings;

namespace Server.OrchestrationInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for RegisterUserOrcRead
    /// </summary>
    public interface IRegisterUserOrcRead
    {
        Task<ResultSqlActionData<RegisterUser>> RegisterUserGetUserByTaz(RegisterUserBasic registerUserBasic);
    }
}
