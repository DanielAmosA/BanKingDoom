import { ITransactionActionBasic } from "../Entity/ITransactionActionBasic";
import { ITransactionActionWithRegisterUserData } from "../Entity/ITransactionActionWithRegisterUserData";

// The interface responsible for Structure declaration
// for TransactionActionList Props
export interface ITransactionActionListProps {
    transactionsAction: ITransactionActionWithRegisterUserData[];
     onEdit: (transactionAction: ITransactionActionBasic) => void;
     onDelete: (id: number) => void;
  }
  