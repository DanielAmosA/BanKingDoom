using Microsoft.AspNetCore.Http.HttpResults;
using Server.Helpers.Validation;
using Server.Helpers.ValidatorInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Drawing;
using System.Dynamic;
using System;
using System.Diagnostics.Metrics;

namespace Server.Helpers.Validator
{
    /// <summary>
    /// The class responsible for allows you to create a system for validating models in a generic way.
    /// This is a generic class designed to validate objects of type TModel. 
    /// TModel is a generic type that represents the model, and must be of type class 
    /// (i.e., an object that relies on a class and not a value type)
    /// </summary>
    public class ValidatorStruct<TModel> : IValidatorStruct<TModel> 
                                            where TModel : class 
    {

        //Stores the values ​​of the fields to be validated.
        private readonly Dictionary<string, object> inputFieldValids = new Dictionary<string, object>();
        
        //Accepts an Expression that refers to a field in the model(of type TModel).
        //It creates a ValidatorInputFieldStruct object for the specific field
        //and returns it, storing it in the inputFieldVals dictionary by field name.
        public ValidatorInputFieldStruct<TModel, TProperty> ValidatorFor<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        where TProperty : notnull
        {
            var validator = new ValidatorInputFieldStruct<TModel, TProperty>(propertyExpression);
            inputFieldValids[validator.GetPropertyName()] = validator;
            return validator;
        }

        //The validation process begins.
        //A ValidationResultStruct object is created to store the result of the validation
        //whether the model is valid (isValid) and the list of errors, if any.

        public ValidationResultStruct Validate(TModel model)
        {
            var result = new ValidationResultStruct
            {
                isValid = true,
                errors = new Dictionary<string, List<string>>()
            };

            //At this point, the function goes through each field in the inputFieldVals dictionary and performs the validation on it.
            //Each field goes through the validation process by calling the Validate function of the appropriate validator.
            //If the validation fails, the result is set to invalid(isValid = false),
            //and the errors are saved.

            foreach (var fieldValidator in inputFieldValids.Values)
            {
                var validatorType = fieldValidator.GetType();
                var validateMethod = validatorType.GetMethod("Validate");
                var validateResult = validateMethod?.Invoke(fieldValidator, new[] { model }) as (bool IsValid, List<string> Errors)?;
                if (validateResult != null && !validateResult.Value.IsValid)
                {
                    result.isValid = false;
                    result.errors[validatorType.GetMethod("GetPropertyName")?.Invoke(fieldValidator, null) as string ?? ""]
                        = validateResult.Value.Errors;
                }
            }

            //After the system has checked all the fields,
            //the function returns the validation result,
            //which includes information about whether the model is correct
            //and whether there are any errors.
            return result;
        }

    }
}
