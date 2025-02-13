import { createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import { CallApiAction } from "../../services/ApiService";
import { ITransactionActionWithRegisterUserData } from "../../Interfaces/Entity/ITransactionActionWithRegisterUserData";
import { ITransactionActionBasic } from "../../Interfaces/Entity/ITransactionActionBasic";
import { ITransactionsActionState } from "../../Interfaces/Structure/ITransactionsActionState";
import { ITransactionActionInsert } from "../../Interfaces/Entity/ITransactionActionInsert";
import { ITransactionActionWithAPIResult } from '../../Interfaces/Entity/ITransactionActionWithAPIResult';

// Defines the initial Redux state for the state of transactional actions.
const initialState: ITransactionsActionState = {
  transactionsAction: [],
  loading: "idle",
  error: null,
};

// Asynchronous action in Redux using createAsyncThunk 
// to retrieve the transaction history of a specific register user.

export const fetchTransactionsAction = createAsyncThunk(
  "transactionsAction/fetchTransactionsAction",

  
  async ({ userID, token }: { userID: string, token: string }, { rejectWithValue }) => {
    try {
      const queryParams = new URLSearchParams({
        userID: userID
      });

      const response = await CallApiAction({
        controller: "Transaction",
        method: "Get",
        service: "TransactionHistoryGetTransactionHistoryByUserID",
        urlParams: queryParams,
        token: token
      });

      if (!response.success) {
        return rejectWithValue(response.error);
      }

      return response.data as ITransactionActionWithRegisterUserData[];
    } catch (error) {
      return rejectWithValue(
        error instanceof Error ? error.message : "An error occurred"
      );
    }
  }
);

// Asynchronous action in Redux using createAsyncThunk 
// to delete a particular transaction.

export const deleteTransactionAction = createAsyncThunk(
  "transactionsAction/deleteTransactionAction",
  
  async ({ id, token }: { id: number, token: string }, { rejectWithValue }) =>  {
    try {
      const queryParams = new URLSearchParams({
        transactionActionID: id.toString(),
      });

      const response = await CallApiAction({
        controller: "Transaction",
        method: "Delete",
        service: "TransactionActionDelete",
        urlParams: queryParams,
        token:token
      });

      if (!response.success) {
        return rejectWithValue(response.error);
      }

      return id;
    } catch (error) {
      return rejectWithValue(
        error instanceof Error ? error.message : "An error occurred"
      );
    }
  }
);

// Asynchronous action in Redux using createAsyncThunk 
// to update a transaction.

export const insertTransaction = createAsyncThunk(
    'transactionsAction/insertTransactionAction',

    async ({ transactionData, token }: { transactionData: ITransactionActionInsert, token: string }, { rejectWithValue }) =>  {
      try {
        const response = await CallApiAction({
          controller: 'Transaction',
          method: 'Post',
          service: 'TransactionActionInsert',
          body: transactionData,
          token:token
        });
  
        if (!response.success) {
          return rejectWithValue(response.error);
        }
  
        return response.data as ITransactionActionWithAPIResult;
      } catch (error) {
        return rejectWithValue(error instanceof Error ? error.message : 'An error occurred');
      }
    }
  );

// Asynchronous action in Redux using createAsyncThunk 
// to update a transaction.

export const updateTransactionAction = createAsyncThunk(
  "transactionsAction/updateTransactionAction",
  
  
  async ({ transactionsAction,taz, token }: { transactionsAction: ITransactionActionBasic,taz: string, token: string }, { rejectWithValue }) =>  {
    try {
        const queryParams = new URLSearchParams({
            taz: taz,
          });

      const response = await CallApiAction({
        controller: "Transaction",
        method: "Put",
        service: "TransactionActionUpdate",
        body: transactionsAction,
        urlParams:queryParams,
        token:token
      });

      if (!response.success) {
        return rejectWithValue(response.error);
      }

      return response.data as ITransactionActionBasic;
    } catch (error) {
      return rejectWithValue(
        error instanceof Error ? error.message : "An error occurred"
      );
    }
  }
);

const transactionActionSlice = createSlice({
  name: "transactionsAction",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder

      // Fetch Transactions
      .addCase(fetchTransactionsAction.pending, (state) => {
        state.loading = "pending";
      })
      .addCase(fetchTransactionsAction.fulfilled, (state, action) => {
        state.loading = "succeeded";
        state.transactionsAction = action.payload;
        state.error = null;
      })
      .addCase(fetchTransactionsAction.rejected, (state, action) => {
        state.loading = "failed";
        state.error = action.payload as string;
      })

      // Delete Transaction
      .addCase(deleteTransactionAction.fulfilled, (state, action) => {
        state.transactionsAction = state.transactionsAction.filter(
          (transactionAction) => transactionAction.id !== action.payload
        );
      })

      // Insert Transaction
      .addCase(insertTransaction.fulfilled, (state, action) => {
        state.transactionsAction.push(action.payload as ITransactionActionWithRegisterUserData);
        state.error = null;
      })
      .addCase(insertTransaction.rejected, (state, action) => {
        state.error = action.payload as string;
      })

      // Update Transaction
      .addCase(updateTransactionAction.fulfilled, (state, action) => {
        const index = state.transactionsAction.findIndex(
          (transactionAction) => transactionAction.id === action.payload.id
        );
        if (index !== -1) {
          state.transactionsAction[index] = {
            ...state.transactionsAction[index],
            ...action.payload,
          };
        }
      });
  },
});

export default transactionActionSlice.reducer;
