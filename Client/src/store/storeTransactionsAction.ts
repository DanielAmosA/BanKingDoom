import { configureStore } from '@reduxjs/toolkit';
import transactionActionReducer from './slices/transactionActionSlice';

// We renamed the store where all the application's data will be stored, 
// and defined the reducer that handles transactions.
export const storeTransactionsAction = configureStore({
    reducer: {
      transactionsAction: transactionActionReducer,
    },
  });

  export type RootTransactionsActionState = ReturnType<typeof storeTransactionsAction.getState>;
  export type TransactionsActionDispatch = typeof storeTransactionsAction.dispatch;