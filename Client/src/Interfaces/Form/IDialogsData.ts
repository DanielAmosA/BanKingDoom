import { dialogActions, kindButtons } from "../../utils/GeneralVar";

// The interface responsible for Structure declaration
// for dialog status
export interface IDialogsData {
    titleSuccess?: string,
    msgSuccess?: string,
    actionSuccess?: dialogActions,
    btnSuccess?: kindButtons,
    showSuccess?: boolean,
    titleWarning?: string,
    msgWarning?: string,
    actionWarning?: dialogActions,
    btnWarning?: kindButtons,
    showWarning?: boolean,
  
}