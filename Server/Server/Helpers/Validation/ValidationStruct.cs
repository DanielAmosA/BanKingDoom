namespace Server.Helpers.Validation
{
    /// <summary>
    /// The class responsible for allows you to check whether an Generic input (of type TValidation) 
    /// is valid by using a custom function 
    /// and save the error message in case of an error.
    /// </summary>
    public class ValidationStruct<TValidation>
    {
        // Accepts an object of type TValidation
        // and returns a boolean value(bool).
        // This function is executed on the TValidation object to check if it is valid.
        public Func<TValidation, bool> validaton { get; }
        public string errorMessage { get; }

        public ValidationStruct(Func<TValidation, bool> validaton, string errorMessage)
        {
            this.validaton = validaton;
            this.errorMessage = errorMessage;
        }
    }
}
