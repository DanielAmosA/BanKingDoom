using Server.Helpers.ServiceInterfaces;
using Server.Helpers.Validation;
using Server.Helpers.Validator;
using Server.Models.Entity;

namespace Server.Helpers.Service
{
    /// <summary>
    /// The class responsible for performs validation on the TransactionActionInsert model 
    /// and verifies that certain fields meet the specified conditions 
    /// </summary>
    public class ValidatorTransactionActionInsert : IValidatorService<TransactionActionInsert>
    {
        private readonly ValidatorStruct<TransactionActionInsert> validator;

        public ValidatorTransactionActionInsert()
        {
            validator = new ValidatorStruct<TransactionActionInsert>();

            validator.ValidatorFor(x => x.Amount)
                .Required()
                .Min(1)
                .Max(999999999);

            validator.ValidatorFor(x => x.BankAccountNumber)
                .Required()
                .MinLength(7)
                .MaxLength(12);

            validator.ValidatorFor(x => x.TransactionType)
                .Required();

            validator.ValidatorFor(x => x.Taz)
                .Required()
                .ValidateTaz();

        }

        public ValidationResultStruct Validate(TransactionActionInsert transactionActionBasic)
        {
            return validator.Validate(transactionActionBasic);
        }
    }
}
