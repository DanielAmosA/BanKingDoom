import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import type {
  RootTransactionsActionState,
  TransactionsActionDispatch,
} from "./storeTransactionsAction";

// Using hooks to access state and dispatch actions
// for TransactionsAction in Redux

export const useTransactionsActionDispatch = () =>
  useDispatch<TransactionsActionDispatch>();
export const useTransactionsActionSelector: TypedUseSelectorHook<RootTransactionsActionState> =
  useSelector;
