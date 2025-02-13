using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using Newtonsoft.Json.Linq;
using Server.Helpers.Validation;
using Server.Helpers.ValidatorInterfaces;
using System.Collections.Generic;
using System;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Server.Helpers.WebEnum;
using System.Net.NetworkInformation;

namespace Server.Helpers.Validator
{

    /// <summary>
    /// The class responsible for store information about a field  (TProperty) in a model (TModel) 
    /// and perform the appropriate validation on it. 
    /// It does this using a functional expression that references the specific field, 
    /// and provides a function to obtain the field name for use in the validation system.
    /// </summary>
    public class ValidatorInputFieldStruct<TModel, TProperty> :IValidatorInputFieldStruct<TModel, TProperty> where TModel : class
                                                                   where TProperty : notnull
    {
        // Store all validations made on the specific field.
        private readonly List<ValidationStruct<TProperty>> validations = new List<ValidationStruct<TProperty>>();
        // The functional expression that references a field in the model.
        private readonly Expression<Func<TModel, TProperty>> expressionFunc;
        // A function obtained from the expression expressionFunc after compilation.
        private readonly Func<TModel, TProperty> func;
        
        // The constructor accepts a functional expression(expression)
        // that references a field in the model and stores it in the expressionFunc field.
        public ValidatorInputFieldStruct(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            expressionFunc = propertyExpression;

            //In addition, the propertyExpression.Compile()
            //function compiles the expression into a function that is stored in func.
            func = propertyExpression.Compile();
        }

        //This function returns the name of the field referenced by the expression.
        public string GetPropertyName()
        {
            //It does this by truncating the body of the expression expressionFunc.Body and calculating the name of the field (Member.Name). 
            MemberExpression? memberExpression = expressionFunc.Body as MemberExpression;

            //If the expression fails to truncate the name, the function returns an empty string (string.Empty).
            return memberExpression?.Member.Name ?? string.Empty;
        }

        // Validates whether the field is not empty or null.
        // If the field is empty or null, an error message is returned.
        public ValidatorInputFieldStruct<TModel, TProperty> Required(string errorMessage = "Required field")
        {
            validations.Add(new ValidationStruct<TProperty>(
                value => value != null && 
                         (!(value is string) || 
                         !string.IsNullOrEmpty(value as string)),
                errorMessage
            ));
            return this;
        }

        // Compares the entered date (value) with the date provided as a parameter (dateTime2),
        // according to the selected dateCompareType.
        private bool IsDateValid(string value, DateTime dateTime2, DateCompareType dateCompareType)
        {
            DateTime currentDate = Convert.ToDateTime(value);

            switch (dateCompareType)
            {
                case DateCompareType.Greater:
                    return DateTime.Compare(currentDate, dateTime2) > 0;

                case DateCompareType.Less:
                    return DateTime.Compare(currentDate, dateTime2) < 0;

                case DateCompareType.Equal:
                    return DateTime.Compare(currentDate, dateTime2) == 0;

                case DateCompareType.GreaterOrEqual:
                    return DateTime.Compare(currentDate, dateTime2) >= 0;

                case DateCompareType.LessOrEqual:
                    return DateTime.Compare(currentDate, dateTime2) <= 0;
                default:
                    return false;
            }
        }

        // Compares an entered date to another date,
        // according to the selected comparison type(greater, less etc.),
        // and returns an error message if the condition is not met.
        public ValidatorInputFieldStruct<TModel, TProperty> ValidateDateCompare(DateTime dateTime2, DateCompareType dateCompareType,
                                                                                    string errorMessage = "The date is invalid " +
                                                                                                          "or does not meet the comparison condition")
        {
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                         string.IsNullOrEmpty(value.ToString()) ||
                         IsDateValid(value.ToString()!, dateTime2, dateCompareType),
                errorMessage
            ));
            return this;
        }

        // Validates whether the length of the value in the field
        // does not exceed the maximum length specified.
        public ValidatorInputFieldStruct<TModel, TProperty> MaxLength(int length, string? errorMessage = null)
        {
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null || (value as string)?.Length <= length,
                errorMessage ?? $"Maximum length is {length} characters"
            ));
            return this;
        }

        // Validates if the length of the value in the field
        // is at least as long as the minimum length specified.
        public ValidatorInputFieldStruct<TModel, TProperty> MinLength(int length, string? errorMessage = null)
        {
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null || (value as string)?.Length >= length,
                errorMessage ?? $"Minimum length is {length} characters"
            ));
            return this;
        }

        // Checks if the value is less than or equal to max
        // Supports strings (by length) and numbers (IComparable)
        // If the value exceeds the maximum, returns an error
        public ValidatorInputFieldStruct<TModel, TProperty> Max(int max, string? errorMessage = null)
        {
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                 (value is string str && str.Length <= max) ||
                 (value is IComparable com && com.CompareTo(max) <= 0),
                  errorMessage ?? $"Value must be less than or equal to {max}"
            ));
            return this;
        }

        // Checks if the value is greater than or equal to min
        // Supports strings (by length) and numbers (IComparable)
        // If the value is less than the minimum, returns an error
        public ValidatorInputFieldStruct<TModel, TProperty> Min(int min, string? errorMessage = null)
        {
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                         (value is string str && str.Length >= min) ||
                         (value is IComparable com && com.CompareTo(min) >= 0),
                errorMessage ?? $"Value must be greater than or equal to {min}"
            ));
            return this;
        }

        // Validates if the value in the field contains only Hebrew letters.
        public ValidatorInputFieldStruct<TModel, TProperty> OnlyHebrewLetters(string errorMessage = "Only Hebrew letters must be entered")
        {
            string patternCheckOnlyHebrewLetters = @"^[א-ת]*$";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                        Regex.IsMatch(value.ToString()!, patternCheckOnlyHebrewLetters),
                errorMessage
            ));
            return this;
        }

        // Validates if the value in the field contains only English letters.
        public ValidatorInputFieldStruct<TModel, TProperty> OnlyEnglishLetters(string errorMessage = "Only English letters must be entered")
        {
            string patternCheckOnlyEnglishLetters = @"^[a-zA-Z]*$";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                        Regex.IsMatch(value.ToString()!, patternCheckOnlyEnglishLetters),
                errorMessage
            ));
            return this;
        }

        // Validates if the value in the field contains only digits.
        public ValidatorInputFieldStruct<TModel, TProperty> OnlyDigits(string errorMessage = "Only digits must be entered")
        {
            string patternCheckOnlyDigits = @"^\d*$";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                        Regex.IsMatch(value.ToString()!, patternCheckOnlyDigits),
                errorMessage
            ));
            return this;
        }

        // Validates if the value in the field contains LeadingZero.
        public ValidatorInputFieldStruct<TModel, TProperty> LeadingZero(string errorMessage = "Must not start with 0")
        {
            string patternCheckLeadingZero = @"^[1-9]\d*$";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                        Regex.IsMatch(value.ToString()!, patternCheckLeadingZero),
                errorMessage
            ));
            return this;
        }


        // Validates if the value in the field contains
        // at least one lowercase English letter.
        public ValidatorInputFieldStruct<TModel, TProperty> RequireLowerCase(string errorMessage = "Must contain at least one lowercase English letter")
        {
            string patternCheckRequireLowerCase = @"[a-z]";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                        Regex.IsMatch(value.ToString()!, patternCheckRequireLowerCase),
                errorMessage
            ));
            return this;
        }

        // Validates if the value in the field contains
        // at least one uppercase English letter.
        public ValidatorInputFieldStruct<TModel, TProperty> RequireUpperCase(string errorMessage = "Must contain at least one capital letter in English")
        {
            string patternCheckRequireUpperCase = @"[A-Z]";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                         Regex.IsMatch(value.ToString()!, patternCheckRequireUpperCase),
                errorMessage
            ));
            return this;
        }

        // Validates if the value in the field contains
        // at least one special character.
        public ValidatorInputFieldStruct<TModel, TProperty> RequireSpecialChar(string specialChars = @"!@#$%^&*()_+\-=\[\]{}|;:,.<>?", 
                                                                               string errorMessage = "Must contain a special character")
        {
            string patternCheckRequireSpecialChar = $"[{specialChars}]";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                         Regex.IsMatch(value.ToString()!, patternCheckRequireSpecialChar),
                errorMessage
            ));
            return this;
        }

        // Validates if the value in the field contains
        // at least one digit.
        public ValidatorInputFieldStruct<TModel, TProperty> RequireDigit(string errorMessage = "Must contain at least one digit")
        {
            string patternCheckRequireDigit = @"\d";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                         Regex.IsMatch(value.ToString()!, patternCheckRequireDigit),
                errorMessage
            ));
            return this;
        }

        // Validates the email address entered using regex to
        // ensure it matches the format of a standard email address.
        public ValidatorInputFieldStruct<TModel, TProperty> ValidateEmail(string errorMessage = "The email address is invalid")
        {
            string patternCheckValidateEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                         string.IsNullOrEmpty(value.ToString()) ||
                         Regex.IsMatch(value.ToString()!, patternCheckValidateEmail),
                errorMessage
            ));
            return this;
        }

        // Check the validity of an identity card (ID - taz) number
        // according to a check digit calculation algorithm common in Israel.
        private bool CheckIsTaz(string taz)
        {
            string patternCheckIsTaz = @"^\d{9}$";
            if (!Regex.IsMatch(taz, patternCheckIsTaz))
                return false;

            // The taz must be 9 digits long.
            // If the entered value contains less than 9 digits,
            // it is completed on the left with a "0"
            // until a number of 9 digits is obtained.

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                // The check digit is calculated by adding a weighted sum of the digits:
                //     Each digit is multiplied by 1 if it is in an even place (0, 2, 4...)
                //     or by 2 if it is in an odd place (1, 3, 5...).

                //     If the result of the multiplication is greater than 9,
                //     9 must be subtracted from it (for example: if the multiplication yields 16 → 16 - 9 = 7).
                //     The total sum of all digits (after the calculation) is stored in the variable sum.

                int digit = int.Parse(taz[i].ToString()) * ((i % 2) + 1);

                if (digit > 9)
                {
                    digit -= 9;
                }
                sum += digit;
            }

            //       The algorithm checks whether the total sum is
            //       divisible by 10 without a remainder (sum % 10 === 0).
            //       If the result is correct, the function returns null (i.e., no error).
            //       If the result is incorrect, it returns the message defined in message.

            return sum % 10 == 0;
        }

        // Validates the Taz number entered,
        // if it is a standard ID number.
        public ValidatorInputFieldStruct<TModel, TProperty> ValidateTaz(string errorMessage = "The taz is invalid")
        {
            validations.Add(new ValidationStruct<TProperty>(
                value => value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                         CheckIsTaz(value.ToString()!),
                errorMessage
            ));
            return this;
        }

        // Validates a field and makes sure it only contains
        // specific selected characters, such as English letters,
        public ValidatorInputFieldStruct<TModel, TProperty> ValidateAllowedCharsOnly(string patternCheckAllowedCharsOnly = @"[a-zA-Z\u0590-\u05FF'\-\s]",
                                                                                     string errorMessage = "Only English / Hebrew letters, apostrophes, hyphens, and spaces are allowed")
        {
            string patternCheckAllowedCharsOnlyToRegex = "^" + patternCheckAllowedCharsOnly + "*$";
            validations.Add(new ValidationStruct<TProperty>(
                value =>
                         value == null ||
                        string.IsNullOrEmpty(value.ToString()) ||
                        Regex.IsMatch(value.ToString()!, patternCheckAllowedCharsOnlyToRegex),
                errorMessage
            ));
            return this;
        }

        // A custom validation that performs the function passed to
        // it as an argument and returns a boolean result.
        public ValidatorInputFieldStruct<TModel, TProperty> Custom(Func<TProperty, bool> validator, string errorMessage)
        {
            validations.Add(new ValidationStruct<TProperty>(validator, errorMessage));
            return this;
        }

        // Checks whether a particular field in a model is valid or not,
        // and returns the validation result along with a list of errors, if any.

        // The function returns a tuple containing two values :
        //      isValid: A boolean value that indicates whether the validation was successful or not.
        //      errors: A list of text strings, where errors are stored, if any.

        // Checks whether a field in a model is valid according to a series of validations.
        // If the validation fails, the errorMessage is added to the list,
        // and at the end the function returns whether the validation was successful or not,
        // along with a list of errors.
        public (bool isValid, List<string> errors) Validate(TModel model)
        {
            // The value that the func function calculates for the model.
            // The func function is the function that results from compiling the previously saved expression.
            var value = func(model);

            //A new list in which errors, if any, are saved during the validation process.
            var errors = new List<string>();

            // The function goes through all validations(the list of previously saved validations),
            // and performs the validation for each of them.
            foreach (var valid in validations)
            {
                //  If the validation(value) function returns false(i.e., the validation failed),
                //  then the errorMessage is added to the errors list.
                if (!valid.validaton(value))
                {
                    errors.Add(valid.errorMessage);
                }
            }

            // The function returns the tuple(bool, List<string>):
            //If there are no errors(the error list is empty),
            //then IsValid will be true(validation passed successfully).
            return (!errors.Any(), errors);
        }
    }
}
