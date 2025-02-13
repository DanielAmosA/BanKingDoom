using Microsoft.AspNetCore.Http.HttpResults;
using Server.BL;
using Server.BLInterfaces;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Settings;
using Server.OrchestrationInterfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.Orchestration
{
    /// <summary>
    /// The class responsible for Managing Write requests 
    /// From the RegisterUserController (API) to the RegisterUserBL (Business Logic).
    /// With using the Factory Design Pattern
    /// </summary>
    public class RegisterUserOrcWrite : IRegisterUserOrcWrite
    {
        private readonly IFactory<RegisterUserBL> registerUserBLFactory;

        public RegisterUserOrcWrite(IFactory<RegisterUserBL> registerUserBLFactory)
        {
            // Inject the registerUserBLFactory,
            // which is a factorization that will provide RegisterUserBL objects.
            // This factory creates the Business Logic(BL) objects.
            this.registerUserBLFactory = registerUserBLFactory;
        }

        public async Task<ResultSqlActionData<RegisterUser>> RegisterUserInsert(RegisterUser registerUser)
        {
            using(RegisterUserBL registerUserBL = registerUserBLFactory.Create())
            {
                return await registerUserBL.RegisterUserInsert(registerUser);
            }        
        }
    }
}
