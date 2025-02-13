using Server.Models.Entity;
using Server.Models.Settings;

namespace Server.DALInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for RegisterUserDAL
    /// </summary>
    public interface IRegisterUserDAL
    {
        Task<ResultSqlActionData<List<RegisterUser>>> RegisterUserInsert(RegisterUser registerUser);
        Task<ResultSqlActionData<List<RegisterUser>>> RegisterUserGetUserByTaz(RegisterUserBasic registerUserBasic);       
    }
}
