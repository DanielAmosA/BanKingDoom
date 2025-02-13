using Server.Helpers.WebEnum;
using Server.Helpers.ServiceInterfaces;
using Server.Helpers.Validation;
using Server.Helpers.Validator;
using Server.Models.Entity;

namespace Server.Helpers.Service
{
    /// <summary>
    /// The class responsible for performs validation on the RegisterUser model 
    /// and verifies that certain fields meet the specified conditions 
    /// </summary>
    public class ValidatorRegisterUserService : IValidatorService<RegisterUser>
    {
        private readonly ValidatorStruct<RegisterUser> validator;

        public ValidatorRegisterUserService()
        {
            validator = new ValidatorStruct<RegisterUser>();

            validator.ValidatorFor(x => x.HebrewFullName)
                .Required()
                .MinLength(2)
                .MaxLength(20)
                .ValidateAllowedCharsOnly(@"[\u0590-\u05FF'\-\s]", "Only Hebrew letters, apostrophes, hyphens, and spaces can be entered.");

            validator.ValidatorFor(x => x.EnglishFullName)
                .Required()
                .MinLength(2)
                .MaxLength(20)
                .ValidateAllowedCharsOnly(@"[a-zA-Z'\-\s]", "Only English letters, apostrophes, hyphens, and spaces can be entered.");

            validator.ValidatorFor(x => x.BirthdayDate)
               .Required()
               .ValidateDateCompare(DateTime.Now, DateCompareType.LessOrEqual, "Date of birthday cannot be greater than the current date.");

            validator.ValidatorFor(x => x.Taz)
                .Required()
                .ValidateTaz();

            validator.ValidatorFor(x => x.Password)
                .Required()
                .RequireUpperCase()
                .RequireLowerCase()
                .RequireDigit()
                .RequireSpecialChar()              
                .MinLength(8)
                .MaxLength(20);

        }

        public ValidationResultStruct Validate(RegisterUser registerUser)
        {
            return validator.Validate(registerUser);
        }
    }
}
