import { typeTransaction } from "../../utils/GeneralVar";
import { IBasic } from "./IBasic";

// The interface responsible for Structure declaration for TransactionAction ( Basic data ) entity
export interface ITransactionActionBasic extends IBasic{
  amount:number;
  bankAccountNumber:string;
  transactionType:typeTransaction;
  }