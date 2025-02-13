import { validateAmount, validateBankAccount, validateFullName, validatePassword } from "./ValidFun";

// Saving the types of log
export type typeLog = 'Error' | 'Info';

// Saving the types of field
export type typeField = 'text' | 'email' | 'password' | 'tel' | 'number' | 'textarea' | 'date' | 'select';

// Saving the types of field
export type typeValidRule = (value: string) => string | null;

// Saving the types of transaction 
export type typeTransaction  = 'Deposit' | 'Withdrawal';

// Saving the types of transaction 
export type typeFilterTransaction  = 'Deposit' | 'Withdrawal' | 'All';

// Saving the types of transactionStatus 
export type typeTransactionStatus  = 'בתהליך' | 'הושלם' | 'נכשל';

export type typeLanguage = 'hebrew' | 'english';

// Saving the type of option of select 
export type typeOption = {
    value: string | number;
    label: string;
  };

// Saving the names of the controllers and their corresponding services.

export type typeController = 'Auth' | 'RegisterUser' | 'Transaction';
export type typeService = 'GenerateToken' |
                             'RegisterUserInsert' | 'RegisterUserGetUserByTaz' |
                             'TransactionActionDelete' | 'TransactionActionInsert' | 'TransactionActionUpdate' |'TransactionHistoryGetTransactionHistoryByUserID';

export type typeMethod = "Get" | "Post" | "Put" | "Delete";

export type typeRole = 'User'
  
 // Saving the data for loads and dialogs                          
export type typeKindsLoadData = 'Wait' | 'Error' | 'NotFound';
export type dialogActions = (data?:unknown,choice?: string, ) => void;
export type kindButtons = 'YesNo' | 'Ok';

// Saving the custom validates     

  export const validateHebrewFullName = validateFullName('hebrew', 2,20, 'שם מלא בעברית אינו תקין');
  export const validateEnglishFullName = validateFullName('english', 2, 15, 'שם מלא באנגלית אינו תקין');
  export const validateStrongPassword = validatePassword(
    10,15,
    true,
    true,
    true,
    true,
    "הסיסמה אינה עומדת בדרישות החוזק"
  );
  export const validateCurrentAmount = validateAmount(1, 10, "הסכום שגוי");
  export const validateCurrentBankAccountNumber = validateBankAccount(7, 10, false, "מספר חשבון הבנק אינו חוקי");