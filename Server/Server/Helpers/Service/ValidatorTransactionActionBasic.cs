using Server.Helpers.ServiceInterfaces;
using Server.Helpers.Validation;
using Server.Helpers.Validator;
using Server.Models.Entity;

namespace Server.Helpers.Service
{
    public class ValidatorTransactionActionBasic : IValidatorService<TransactionActionBasic>
    {
        private readonly ValidatorStruct<TransactionActionBasic> validator;

        public ValidatorTransactionActionBasic()
        {
            validator = new ValidatorStruct<TransactionActionBasic>();

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
        }

        public ValidationResultStruct Validate(TransactionActionBasic transactionActionBasic)
        {
            return validator.Validate(transactionActionBasic);
        }
    }
}
