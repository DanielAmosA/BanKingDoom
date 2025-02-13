using Server.BL;
using Server.BL.Factory;
using Server.BLInterfaces;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.Entity;
using Server.ModelsInterfaces.Settings;
using Server.OrchestrationInterfaces;

namespace Server.Orchestration
{

    /// <summary>
    /// The class responsible for Managing Read requests 
    /// From the RegisterUserController (API) to the RegisterUserBL (Business Logic).
    /// With using the Factory Design Pattern
    /// </summary>
    public class RegisterUserOrcRead: IRegisterUserOrcRead
    {
        private readonly IFactory<RegisterUserBL> registerUserBLFactory;

        public RegisterUserOrcRead(IFactory<RegisterUserBL> registerUserBLFactory)
        {
            // Inject the registerUserBLFactory,
            // which is a factorization that will provide RegisterUserBL objects.
            // This factory creates the Business Logic(BL) objects.
            this.registerUserBLFactory = registerUserBLFactory;
        }

        public async Task<ResultSqlActionData<RegisterUser>> RegisterUserGetUserByTaz(RegisterUserBasic registerUserBasic)
        {
            using (RegisterUserBL registerUserBL = registerUserBLFactory.Create())
            {
                return await registerUserBL.RegisterUserGetUserByTaz(registerUserBasic);
            }
        }
    }
}
