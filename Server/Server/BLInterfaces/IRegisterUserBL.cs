using Server.Models.Entity;
using Server.Models.Settings;

namespace Server.BLInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for RegisterUserBL
    /// </summary>
    public interface IRegisterUserBL : IDisposable
    {
        Task<ResultSqlActionData<RegisterUser>> RegisterUserInsert(RegisterUser registerUser);
        Task<ResultSqlActionData<RegisterUser>> RegisterUserGetUserByTaz(RegisterUserBasic registerUserBasic);
    }
}
