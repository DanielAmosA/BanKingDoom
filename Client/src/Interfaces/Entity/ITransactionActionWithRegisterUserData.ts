import { ITransactionActionWithAPIResult } from "./ITransactionActionWithAPIResult";

// The interface responsible for Structure declaration for TransactionAction 
// ( With extra data related to the API ( openBanking ) result ) entity
export interface ITransactionActionWithRegisterUserData extends ITransactionActionWithAPIResult {
    hebrewFullName:string;
    englishFullName:string;
  }