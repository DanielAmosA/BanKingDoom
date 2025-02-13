import { ITransactionActionBasic } from "../Entity/ITransactionActionBasic";
import { ITransactionActionWithRegisterUserData } from "../Entity/ITransactionActionWithRegisterUserData";

// The IDashboardProps interface responsible for Structure declaration
// for Dashboard Props
export interface IDashboardProps {
    transactions: ITransactionActionWithRegisterUserData[];
    onTransactionUpdate: (transaction: ITransactionActionBasic) => Promise<void>;
    onTransactionDelete: (id: string) => Promise<void>;
    onDeposit: (amount: number) => Promise<void>;
    onWithdrawal: (amount: number) => Promise<void>;
  }