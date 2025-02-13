using Server.Helpers.Validation;

namespace Server.Helpers.ValidatorInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for ValidatorStruct
    /// </summary>
    public interface IValidatorStruct<TModel> where TModel : class
    {
        ValidationResultStruct Validate(TModel model);
    }
}
