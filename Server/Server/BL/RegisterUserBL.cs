using Server.BLInterfaces;
using Server.DAL;
using Server.DALInterfaces;
using Server.Helpers.Service;
using Server.Helpers.ServiceInterfaces;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.BL
{
    /// <summary>
    /// The class responsible for Register User Business Logic 
    /// Before being sent to the RegisterUserDAL (Data Access Layer).
    /// </summary>
    public class RegisterUserBL : IRegisterUserBL
    {
        private readonly IRegisterUserDAL registerUserDAL;
        private readonly ISecurityService securityService;

        public RegisterUserBL(IRegisterUserDAL registerUserDAL, ISecurityService securityService)
        {
            // Calling and executing services of the DAL (Data Access Layer).
            this.registerUserDAL = registerUserDAL;
            this.securityService = securityService;
        }

        public async Task<ResultSqlActionData<RegisterUser>> RegisterUserInsert(RegisterUser registerUser)
        {
            if (registerUser.CreatedAt == default)
                registerUser.CreatedAt = DateTime.Now;
            registerUser.Password = AppService.HashPassword(registerUser.Password);
            registerUser.Taz = securityService.CreateEncryptorValue(registerUser.Taz);
            ResultSqlActionData<List<RegisterUser>> registerUsers = await registerUserDAL.RegisterUserInsert(registerUser);
            return AppService.ProcessResGetFirstRow(registerUsers);
        }

        public async Task<ResultSqlActionData<RegisterUser>> RegisterUserGetUserByTaz(RegisterUserBasic registerUserBasic)
        {
            registerUserBasic.Taz = securityService.CreateEncryptorValue(registerUserBasic.Taz);
            ResultSqlActionData<List<RegisterUser>> resRegisterUsersGetUserByTaz = await registerUserDAL.RegisterUserGetUserByTaz(registerUserBasic);
            ResultSqlActionData<RegisterUser> resRegisterUserGetUserByTaz =  AppService.ProcessResGetFirstRow(resRegisterUsersGetUserByTaz);
            if(resRegisterUserGetUserByTaz.Data != null)
            {
                if (AppService.VerifyPassword(registerUserBasic.Password, resRegisterUserGetUserByTaz.Data.Password))
                {
                    resRegisterUserGetUserByTaz.Data.Taz = securityService.GetDecryptValue(resRegisterUserGetUserByTaz.Data.Taz);
                    return ResultSqlActionData<RegisterUser>.InSuccess(resRegisterUserGetUserByTaz.Data);
                }
                else
                {
                    return ResultSqlActionData<RegisterUser>.InError("Incorrect password");
                }
            }

            else
            {
                return ResultSqlActionData<RegisterUser>.InError("Taz not found");
            }          
        }

        public void Dispose()
        {
            // Makes sure that there is no need to call the class's finalizer,
            // so that the GC will not consume any more resource time on its operation.
            GC.SuppressFinalize(this);
        }
    }
}
