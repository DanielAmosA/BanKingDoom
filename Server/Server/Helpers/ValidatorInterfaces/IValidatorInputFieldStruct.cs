using Server.Helpers.WebEnum;
using Server.Helpers.Validator;

namespace Server.Helpers.ValidatorInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for ValidatorInputFieldStruct
    /// </summary>
    public interface IValidatorInputFieldStruct<TModel, TProperty> where TModel : class 
                                                                   where TProperty : notnull
    {


        string GetPropertyName();

        ValidatorInputFieldStruct<TModel, TProperty> Required(string errorMessage = "Required field");


        ValidatorInputFieldStruct<TModel, TProperty> ValidateDateCompare(DateTime dateTime2, DateCompareType dateCompareType,
                                                                                    string errorMessage = "The date is invalid " +
                                                                                                          "or does not meet the comparison condition");
        ValidatorInputFieldStruct<TModel, TProperty> MaxLength(int length, string? errorMessage = null);

        ValidatorInputFieldStruct<TModel, TProperty> MinLength(int length, string? errorMessage = null);

        ValidatorInputFieldStruct<TModel, TProperty> OnlyHebrewLetters(string errorMessage = "Only Hebrew letters must be entered");

        ValidatorInputFieldStruct<TModel, TProperty> OnlyEnglishLetters(string errorMessage = "Only English letters must be entered");

        ValidatorInputFieldStruct<TModel, TProperty> OnlyDigits(string errorMessage = "Only digits must be entered");

        ValidatorInputFieldStruct<TModel, TProperty> LeadingZero(string errorMessage = "Must not start with 0");

        ValidatorInputFieldStruct<TModel, TProperty> RequireLowerCase(string errorMessage = "Must contain at least one lowercase English letter");

        ValidatorInputFieldStruct<TModel, TProperty> RequireUpperCase(string errorMessage = "Must contain at least one capital letter in English");

        ValidatorInputFieldStruct<TModel, TProperty> RequireSpecialChar(string specialChars = @"!@#$%^&*()_+\-=\[\]{}|;:,.<>?",
                                                                               string errorMessage = "Must contain a special character");

        ValidatorInputFieldStruct<TModel, TProperty> RequireDigit(string errorMessage = "Must contain at least one digit");

        ValidatorInputFieldStruct<TModel, TProperty> ValidateEmail(string errorMessage = "The email address is invalid");

        ValidatorInputFieldStruct<TModel, TProperty> ValidateTaz(string errorMessage = "The taz is invalid");

        ValidatorInputFieldStruct<TModel, TProperty> ValidateAllowedCharsOnly(string patternCheckAllowedCharsOnly = @"[a-zA-Z\u0590-\u05FF'\-\s]",
                                                                                     string errorMessage = "Only English / Hebrew letters, apostrophes, hyphens, and spaces are allowed");

        ValidatorInputFieldStruct<TModel, TProperty> Custom(Func<TProperty, bool> validator, string errorMessage);

        (bool isValid, List<string> errors) Validate(TModel model);



    }
}
