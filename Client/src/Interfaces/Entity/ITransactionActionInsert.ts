import { ITransactionActionWithAPIResult } from "./ITransactionActionWithAPIResult";

// The interface responsible for Structure declaration for TransactionAction 
// ( With extra data related to the Insert action ) entity
export interface ITransactionActionInsert extends ITransactionActionWithAPIResult {
    taz:string;
  }