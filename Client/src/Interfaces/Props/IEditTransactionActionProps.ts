import { ITransactionActionBasic } from "../Entity/ITransactionActionBasic";

// The IDashboardProps interface responsible for Structure declaration
// for EditTransactionAction Props
export interface IEditTransactionActionProps {
    transactionAction: ITransactionActionBasic;
    onSave: (transactionAction: ITransactionActionBasic) => void;
    onCancel: () => void;
  }