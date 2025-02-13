import { ITransactionActionWithRegisterUserData } from "../Entity/ITransactionActionWithRegisterUserData";

// The interface responsible for Structure declaration
// for TransactionAction Props
export interface ITransactionActionProps {
    transaction?: ITransactionActionWithRegisterUserData;
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (transaction: ITransactionActionWithRegisterUserData) => Promise<void>;
  }