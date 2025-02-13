using Server.Helpers.Validation;

namespace Server.Helpers.ServiceInterfaces
{
    /// <summary>
    /// The interface responsible for Generic Structure declaration for ValidatorService
    /// </summary>
    public interface IValidatorService<TModel> where TModel : class
    {
        ValidationResultStruct Validate(TModel model);
    }
}
