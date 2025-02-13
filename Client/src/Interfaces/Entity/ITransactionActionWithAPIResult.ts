import { ITransactionActionBasic } from "./ITransactionActionBasic";

// The interface responsible for Structure declaration for TransactionAction 
// ( With extra data related to the API ( openBanking ) result ) entity
export interface ITransactionActionWithAPIResult extends ITransactionActionBasic {
    statusAction?:string;
    tokenResponse?:string;
  }