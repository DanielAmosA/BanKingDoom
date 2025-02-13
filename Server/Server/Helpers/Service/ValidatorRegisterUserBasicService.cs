using Server.Helpers.WebEnum;
using Server.Helpers.ServiceInterfaces;
using Server.Helpers.Validation;
using Server.Helpers.Validator;
using Server.Models.Entity;

namespace Server.Helpers.Service
{
    /// <summary>
    /// The class responsible for performs validation on the RegisterUser model (basic data)
    /// and verifies that certain fields meet the specified conditions 
    /// </summary>
    public class ValidatorRegisterUserBasicService : IValidatorService<RegisterUserBasic>
    {
        private readonly ValidatorStruct<RegisterUserBasic> validator;

        public ValidatorRegisterUserBasicService()
        {
            validator = new ValidatorStruct<RegisterUserBasic>();

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

        public ValidationResultStruct Validate(RegisterUserBasic registerUserBasic)
        {
            return validator.Validate(registerUserBasic);
        }
    }
}
