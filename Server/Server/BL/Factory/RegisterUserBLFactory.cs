using Server.DALInterfaces;
using Server.Helpers.ServiceInterfaces;
using Server.ModelsInterfaces.Settings;

namespace Server.BL.Factory
{
    /// <summary>
    /// The class responsible for Implementation of the Factory Design Pattern for the RegisterUserBL (Business Logic) 
    /// where the factory is responsible for creating an object of type RegisterUserBL.
    /// </summary>
    public class RegisterUserBLFactory : IFactory<RegisterUserBL>
    {
        private readonly IRegisterUserDAL registerUserDAL;
        private readonly ISecurityService securityService;

        // Gets the required dependencies through the constructor.
        public RegisterUserBLFactory(IRegisterUserDAL registerUserDAL, ISecurityService securityService)
        {
            this.registerUserDAL = registerUserDAL;
            this.securityService = securityService;
        }

        // Constructs the object and returns it, with all injected dependencies.
        public RegisterUserBL Create()
        {
            return new RegisterUserBL(registerUserDAL, securityService);
        }
    }
}
