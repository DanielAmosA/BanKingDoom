using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Server.ModelsInterfaces.Entity;

namespace Server.Models.Entity
{
    /// <summary>
    /// The class responsible for Structure declaration for RegisterUser Entity
    /// </summary>
    public class RegisterUser : RegisterUserBasic, IRegisterUser
    {   
        public string HebrewFullName { get; set; }     

        public string EnglishFullName { get; set; }
    
        public DateTime BirthdayDate { get; set; }
        
        public RegisterUser() : base()
        {
            HebrewFullName = string.Empty;
            EnglishFullName = string.Empty;         
        }
    }
}
