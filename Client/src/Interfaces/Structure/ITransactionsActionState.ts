import { ITransactionActionWithRegisterUserData } from "../Entity/ITransactionActionWithRegisterUserData";

// The interface responsible for Structure declaration
// for TransactionsAction State

export interface ITransactionsActionState {
    transactionsAction: ITransactionActionWithRegisterUserData[];
    loading: 'idle' | 'pending' | 'succeeded' | 'failed';
    error: string | null;
}
