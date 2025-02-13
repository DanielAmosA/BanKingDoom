import { useEffect, useState } from 'react';
import './Dashboard.scss';
import { BirthdayMessage } from '../../features/BirthdayMessage/BirthdayMessage';
import { UserInfo } from '../../features/UserInfo/UserInfo';
import { dialogActions, kindButtons, typeFilterTransaction } from '../../utils/GeneralVar';
// import { dialogActions, kindButtons, typeFilterTransaction, typeKindsLoadData } from '../../utils/GeneralVar';
// import { ShowKindLoadData } from '../../components/WebTools/ShowKindLoadData';
// import { ShowSuccessDialog } from '../../components/WebTools/ShowSuccessDialog';
// import { ShowWarningDialog } from '../../components/WebTools/ShowWarningDialog';
// import { CallApiAction } from '../../services/ApiService';
import { AppAuthData } from '../../App';
// import { ITransactionActionWithRegisterUserData } from '../../Interfaces/Entity/ITransactionActionWithRegisterUserData';
// import { ITransactionActionListProps } from '../../Interfaces/Props/ITransactionActionListProps';
import { ITransactionActionBasic } from '../../Interfaces/Entity/ITransactionActionBasic';
import { EditTransactionAction } from '../../features/EditTransactionAction/EditTransactionAction';
import { TransactionActionList } from '../../features/TransactionActionList/TransactionActionList';
// import { IEditTransactionActionProps } from '../../Interfaces/Props/IEditTransactionActionProps';
import { useTransactionsActionDispatch, useTransactionsActionSelector } from '../../store/hooksTransactionsAction';
import { deleteTransactionAction, fetchTransactionsAction, updateTransactionAction } from '../../store/slices/transactionActionSlice';
import { ShowKindLoadData } from '../../components/WebTools/ShowKindLoadData';
import { ShowWarningDialog } from '../../components/WebTools/ShowWarningDialog';
import { ShowSuccessDialog } from '../../components/WebTools/ShowSuccessDialog';

export const Dashboard = () => {

  // Store And Provider

  const { registerUserWebDataOnly } = AppAuthData();
  const dispatch = useTransactionsActionDispatch();

  const transactionsAction = useTransactionsActionSelector(state => state.transactionsAction.transactionsAction);
  const loadingTransactionsAction = useTransactionsActionSelector(state => state.transactionsAction.loading);
  const errorTransactionsAction = useTransactionsActionSelector(state => state.transactionsAction.error);


  //#region hooks

  // TransactionAction action

  const [modeFilterTransactionsAction, setModeFilterTransactionsAction] = useState<typeFilterTransaction>("All");
  const [editingTransactionAction, setEditingTransactionAction] = useState<ITransactionActionBasic | null>(null);
  const [openEditingTransactionAction, setOpenEditingTransactionAction] = useState<boolean>(false);

  // Dialog data

  const [titleSuccess, setTitleSuccess] = useState<string>('');
  const [msgSuccess, setMsgSuccess] = useState<string>('');
  const [actionSuccess, setActionSuccess] = useState<dialogActions>(() => { });
  const [btnSuccess, setBtnSuccess] = useState<kindButtons>('Ok');
  const [showSuccess, setShowSuccess] = useState<boolean>(false);

  const [titleWarning, setTitleWarning] = useState<string>('');
  const [msgWarning, setMsgWarning] = useState<string>('');
  const [actionWarning, setActionWarning] = useState<dialogActions>(() => { });
  const [btnWarning, setBtnWarning] = useState<kindButtons>('Ok');
  const [showWarning, setShowWarning] = useState<boolean>(false);

  //#endregion

  useEffect(() => {

    if (registerUserWebDataOnly?.id) {
      dispatch(fetchTransactionsAction({userID:registerUserWebDataOnly.id.toString(),token:registerUserWebDataOnly!.token}));
    }
  }, [dispatch, registerUserWebDataOnly, registerUserWebDataOnly?.id]);


  const handleAskDeleteAction = async (id: number) => {
    setTitleWarning("אזהרת פעולת מחיקה ⚠️");
    setMsgWarning("הפעולה תמחק האם להמשיך ?");
    setBtnWarning("YesNo");
    setActionWarning(() => (choice: string) => GetUserChoiceOnDeleteWarning(choice, id));
    setShowWarning(true);
  }

  const GetUserChoiceOnDeleteWarning = async (choice: string, id: number) => {
    setShowWarning(false);
    if (choice === "yes") {
      await dispatch(deleteTransactionAction({id:id,token:registerUserWebDataOnly!.token})).unwrap();
    }
  }

  const handleStartUpdateTransactionAction = async (transactionActionBasic: ITransactionActionBasic) => {
    try {
      await dispatch(updateTransactionAction({transactionsAction:transactionActionBasic,taz:registerUserWebDataOnly!.taz , token:registerUserWebDataOnly!.token})).unwrap();
      setOpenEditingTransactionAction(false);
      setTitleSuccess("עדכון צלח ❇️");
      setMsgSuccess("הפעולה עודכנה בהצלחה");
      setShowSuccess(true);
      setBtnSuccess("Ok");
      setActionSuccess(() => GetUserChoiceOnUpdateSuccess);
    } 
    catch (error) {
            const errorString =
            error instanceof Error ? error.message :
          typeof error === "string" ? error : "An unknown error has occurred.";
      setTitleWarning("שגיאת עדכון פעולה 📛");
      setMsgWarning(errorString);
      setBtnWarning("Ok");
      setActionWarning(() => GetUserChoiceOnError);
      setShowWarning(true);
    }
  };

    const GetUserChoiceOnUpdateSuccess = (): void => {
    setShowSuccess(false);
  }

    const GetUserChoiceOnError = (): void => {
    setShowWarning(false);
  }

  const handleOpenUpdateTransactionAction = (transactionActionBasic: ITransactionActionBasic) => {
    setEditingTransactionAction(transactionActionBasic);
    setOpenEditingTransactionAction(true);
  };

  const filteredTransactionsAction = modeFilterTransactionsAction === "All"
    ? transactionsAction
    : transactionsAction.filter((transactionAction) =>
      transactionAction.transactionType === modeFilterTransactionsAction
    );

  if (loadingTransactionsAction === 'pending') {
    return ShowKindLoadData('Wait');
  }

  if (errorTransactionsAction) {
    return <div>
      {ShowKindLoadData('Error')}
      {errorTransactionsAction}
    </div>;
  }

  //#endregion

  return (
    <div className="page dashboard">
      <BirthdayMessage />
      <br />
      <UserInfo />

      {
        transactionsAction.length > 0 ?
          (
            <>
              <div className="dashboardFilterBtns">
                <button onClick={() => setModeFilterTransactionsAction("All")} className={`dashboardFilterBtn ${modeFilterTransactionsAction === "All" ? "dashboardFilterBtnActive" : ""}`}>
                  ✨ הכל
                </button>
                <button onClick={() => setModeFilterTransactionsAction("Deposit")} className={`dashboardFilterBtn ${modeFilterTransactionsAction === "Deposit" ? "dashboardFilterBtnActive" : ""}`}>
                  💰 הפקדות
                </button>
                <button onClick={() => setModeFilterTransactionsAction("Withdrawal")} className={`dashboardFilterBtn ${modeFilterTransactionsAction === "Withdrawal" ? "dashboardFilterBtnActive" : ""}`}>
                  🏧 משיכות
                </button>
              </div>
              <TransactionActionList transactionActionListProps={{
                transactionsAction: filteredTransactionsAction,
                onEdit: handleOpenUpdateTransactionAction,
                onDelete: handleAskDeleteAction,
              }} />
            </>
          )
          :
          ShowKindLoadData("NotFound")
      }

      {
        openEditingTransactionAction && editingTransactionAction
        && (
          <EditTransactionAction
            editTransactionActionProps={{
              transactionAction: editingTransactionAction,
              onSave: handleStartUpdateTransactionAction,
              onCancel: () => setOpenEditingTransactionAction(false),
            }}
          />
        )
      }

      {
        showWarning &&
        (
          ShowWarningDialog(titleWarning,
            <>{msgWarning}</>,
            actionWarning,
            btnWarning
          )
        )
      }

      {
        showSuccess &&
        (
          ShowSuccessDialog(titleSuccess,
            <>{msgSuccess}</>,
            actionSuccess,
            btnSuccess
          )
        )
      }
    </div>
  );
}
