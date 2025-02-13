using Server.Models.Entity;
using Server.Models.Settings;

namespace Server.OrchestrationInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for RegisterUserOrcWrite
    /// </summary>
    public interface IRegisterUserOrcWrite
    {
        Task<ResultSqlActionData<RegisterUser>> RegisterUserInsert(RegisterUser registerUser);
    }
}
