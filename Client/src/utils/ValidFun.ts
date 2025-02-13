import { typeLanguage, typeValidRule } from "./GeneralVar";

// This is a factory function that returns a validation function.
// Its parameters:
//                 datesCompareAct: A function that compares an input date (inputDate)
//                                  to the current date (today) and returns a boolean value (true/false).
//                 message: An error message to display if the condition is not met.

// The returned function :  takes a string input value (value), converts it to a date object.
//                           Creates a Date object for the current date.
//                           Converts value to a date using new Date(value).
//                            Returns the error message if the condition defined in datesCompareAct is not met.
//                           Otherwise, it returns null (no error).
export const validateDate =
  (
    datesCompareAct: (inputDate: Date, today: Date) => boolean,
    message: string
  ) =>
  (value: string) => {
    const today = new Date();
    const inputDate = new Date(value);

    return datesCompareAct(inputDate, today) ? message : null;
  };

// This is a validation function created based on validateDate.
// Checks that the input date (inputDate)
// is not less than the current date (not in the past).
// If this condition is not met,
// it returns the message.

export const validateDateNotBeforeToday = validateDate(
  (inputDate, today) => inputDate < today,
  "תאריך לא יכול להיות קטן מהתאריך הנוכחי"
);

// This is a validation function created based on validateDate.
// Checks that the input date (inputDate)
// is not greater than the current date (not in the future).
// If this condition is not met,
// it returns the message.

export const validateDateNotAfterToday = validateDate(
  (inputDate, today) => inputDate > today,
  "תאריך לא יכול להיות גדול מהתאריך הנוכחי"
);

// Check the validity of a full name according to certain rules,
// such as language (Hebrew/English), minimum and maximum length, and valid characters.

export const validateFullName =
  (
    language: typeLanguage,
    minLength: number = 1,
    maxLength: number = 10,
    message = "שם מלא אינו תקין"
  ) =>
  (value: string) => {
    const hebrewRegex = /^[א-ת'\- ]+$/;
    const englishRegex = /^[a-zA-Z'\- ]+$/;

    switch (language) {
      case "hebrew": {
        if (!hebrewRegex.test(value)) {
          return `${message}: ניתן להזין רק אותיות בעברית, גרש, מקף ורווח`;
        }
        break;
      }
      case "english": {
        if (!englishRegex.test(value)) {
          return `${message}: ניתן להזין רק אותיות באנגלית, גרש, מקף ורווח`;
        }
        break;
      }
    }

    if (value.length > maxLength) {
      return `${message}: האורך המקסימלי הוא ${maxLength} תווים`;
    }

    if (value.length < minLength) {
      return `${message}: האורך המינמלי הוא ${minLength} תווים`;
    }

    return null;
  };

// Check the validity of an identity card (ID - taz) number
// according to a check digit calculation algorithm common in Israel.

export const validateTaz =
  (message = "מספר תעודת זהות אינו תקין") =>
  (value: string) => {
    if (!/^\d{9}$/.test(value)) {
      return message;
    }

    // The taz must be 9 digits long.
    // If the entered value contains less than 9 digits,
    // it is completed on the left with a "0"
    // until a number of 9 digits is obtained.

    const tazWithZero = value.padStart(9, "0");
    let sum = 0;

    for (let i = 0; i < 9; i++) {
      // The check digit is calculated by adding a weighted sum of the digits:
      //     Each digit is multiplied by 1 if it is in an even place (0, 2, 4...)
      //     or by 2 if it is in an odd place (1, 3, 5...).

      //     If the result of the multiplication is greater than 9,
      //     9 must be subtracted from it (for example: if the multiplication yields 16 → 16 - 9 = 7).
      //     The total sum of all digits (after the calculation) is stored in the variable sum.

      let digit = parseInt(tazWithZero[i]) * (i % 2 === 0 ? 1 : 2);
      if (digit > 9) {
        digit -= 9;
      }
      sum += digit;
    }

    //       The algorithm checks whether the total sum is
    //       divisible by 10 without a remainder (sum % 10 === 0).
    //        If the result is correct, the function returns null (i.e., no error).
    //          If the result is incorrect, it returns the message defined in message.
    return sum % 10 === 0 ? null : message;
  };

// Check the validity of an email address (Email)
// according to a specific pattern (Regular Expression).

export const validateEmail =
  (message = "כתובת האימייל אינה תקינה") =>
  (value: string) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(value) ? null : message;
  };

//  Check the validity of the password according to custom rules,
// such as minimum and maximum length, use of uppercase letters,
// lowercase letters, special characters, and more.

export const validatePassword =
  (
    minLength: number = 8,
    maxLength: number = 20,
    requireUppercase: boolean = true,
    requireLowercase: boolean = true,
    requireSpecialChar: boolean = true,
    requireOnlyEnglishLetters: boolean = true,
    message: string = "הסיסמה אינה עומדת בדרישות"
  ) =>
  (value: string) => {

    if (value.length < minLength) {
      return `${message}: הסיסמה חייבת להיות לפחות ${minLength} תווים`;
    }

    if (value.length > maxLength) {
      return `${message}: הסיסמה חייבת להיות עד ${maxLength} תווים`;
    }

    if (requireUppercase && !/[A-Z]/.test(value)) {
      return `${message}: הסיסמה חייבת להכיל לפחות אות אחת באנגלית גדולה`;
    }

    if (requireLowercase && !/[a-z]/.test(value)) {
      return `${message}: הסיסמה חייבת להכיל לפחות אות אחת באנגלית קטנה`;
    }

    if (requireSpecialChar && !/[^a-zA-Z0-9]/.test(value)) {
      return `${message}: הסיסמה חייבת להכיל לפחות תו מיוחד (כמו !@#$%)`;
    }

    if (
      requireOnlyEnglishLetters &&
      !/^[a-zA-Z0-9!@#$%^&*()_+=\-{}[\]:;"'<>,.?/|\\~`]+$/.test(value)
    ) {
      return `${message}: הסיסמה יכולה להכיל רק תווים באנגלית`;
    }

    return null;
  };

// Check the validity of an Amount value according to certain rules,
// such as minimum and maximum numerical length,
// and additional conditions on the structure.

export const validateAmount =
  (
    minLength: number = 1,
    maxLength: number = 10,
    message: string = "הסכום אינו תקין"
  ) =>
  (value: string) => {
    if (
      !/^\d+$/.test(value) ||
      (value.length > 1 && value.startsWith("0"))
    ) {
      return `${message}: הסכום חייב להיות מספר חיובי שאינו מתחיל ב-0`;
    }

    if (value.length < minLength || value.length > maxLength) {
      return `${message}: יש להזין בין ${minLength} ל-${maxLength} ספרות בלבד`;
    }

    return null;
  };

// Check the validity of a bank account number according to custom rules,
// such as minimum and maximum length, and permission for leading zeros.

export const validateBankAccount =
  (
    minLength: number = 7,
    maxLength: number = 12,
    allowLeadingZero: boolean = false,
    message: string = "מספר חשבון הבנק אינו תקין"
  ) =>
  (value: string) => {

    if (!/^\d+$/.test(value)) {
      return `${message}: מספר החשבון חייב להיות מורכב ממספרים בלבד, ללא רווחים`;
    }

    if (
      !allowLeadingZero &&
      value.length > 1 &&
      value.startsWith("0")
    ) {
      return `${message}: מספר החשבון אינו יכול להתחיל ב-0`;
    }

    if (value.length < minLength || value.length > maxLength) {
      return `${message}: מספר החשבון חייב להכיל בין ${minLength} ל-${maxLength} ספרות`;
    }

    return null;
  };

// Generic mechanism for checking different values.
// Based on the field type (name) and the value (value).

export const validateField = (
  name: string,
  value: string | number
): string | null => {
  const valueAsString = typeof value === "number" ? value.toString() : value;
  switch (name) {
    case "fullNameHebrew":
      return validateFullName(
        "hebrew",
        2,
        20,
        "שם מלא בעברית אינו תקין"
      )(valueAsString);
    case "fullNameEnglish":
      return validateFullName(
        "english",
        2,
        15,
        "שם מלא באנגלית אינו תקין"
      )(valueAsString);
    case "amount":
      return validateAmount(1, 10, "הסכום שגוי")(valueAsString);
    case "bankAccount":
      return validateBankAccount(
        7,
        10,
        false,
        "מספר חשבון הבנק אינו חוקי"
      )(valueAsString);
    default:
      break;
  }
  return "";
};

// Create a modular validation system where you can pass multiple rules
// and check a value against all of them. If any of the validations fails,
// the function will return the first error message.

export const createValid =
  (rules: typeValidRule[]) =>
  (value: string): string | null => {
    for (const rule of rules) {
      const error = rule(value);
      if (error) return error;
    }
    return null;
  };
