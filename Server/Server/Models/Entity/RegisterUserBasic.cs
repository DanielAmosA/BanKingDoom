using Server.ModelsInterfaces.Entity;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for RegisterUser Entity (Basic Data)
    /// </summary>
    public class RegisterUserBasic : Basic, IRegisterUserBasic
    {      
        public string Taz { get; set; }        
        public string Password { get; set; }

        public RegisterUserBasic()
        {
            Taz = string.Empty;
            Password = string.Empty;
        }

    }
}
