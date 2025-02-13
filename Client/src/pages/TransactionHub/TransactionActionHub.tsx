import { useRef } from 'react';
import { WebForm } from '../../components/WebForm/WebForm';
import { IFormField } from '../../Interfaces/Form/IFormField';
import { typeTransaction, validateCurrentAmount, validateCurrentBankAccountNumber } from '../../utils/GeneralVar';
import './TransactionActionHub.scss';
import { IDialogsData } from '../../Interfaces/Form/IDialogsData';
import { AppAuthData } from '../../App';
import { ITransactionActionInsert } from '../../Interfaces/Entity/ITransactionActionInsert';
import { useTransactionsActionDispatch } from '../../store/hooksTransactionsAction';
import { insertTransaction } from '../../store/slices/transactionActionSlice';

export const TransactionActionHub = () => {

  // Store
  const transactionsActionDispatch = useTransactionsActionDispatch();

  // Provider
  const { registerUserWebDataOnly } = AppAuthData();
  const newUpdateDialogsDataRef = useRef<((status: IDialogsData) => void) | null>(null);

  const fields: IFormField[] = [
    {
      name: 'amount',
      label: 'סכום',
      type: 'number',
      required: true,
      validations: [validateCurrentAmount],
      placeholder: 'הכנס סכום'
    },

    {
      name: 'bankAccountNumber',
      label: 'מספר חשבון בנק',
      type: 'text',
      required: true,
      validations: [validateCurrentBankAccountNumber],
      placeholder: 'הכנס מספר חשבון'
    },

    {
      name: "transactionType",
      label: 'פעולה',
      type: "select",
      required: true,
      options: [
        { value: "Deposit", label: 'הפקדה' },
        { value: "Withdrawal", label: 'משיכה' }
      ]
    }
  ];

  const GetUserChoiceOnError = (): void => {
    if (newUpdateDialogsDataRef.current) {
      newUpdateDialogsDataRef.current(
        {
          showWarning: false
        })
    }
  }

  const GetUserChoiceOnSuccess = (): void => {
    if (newUpdateDialogsDataRef.current) {
      newUpdateDialogsDataRef.current(
        {
          showSuccess: false
        });
    }
  }

  const handleSubmit = async (formData: FormData): Promise<void> => {
    try {
      const registerUserTaz = registerUserWebDataOnly?.taz;
      const newAmount = formData.get("amount") as unknown as number;
      const newBankAccountNumber = formData.get("bankAccountNumber") as string;
      const newTransactionType = formData.get("transactionType") as typeTransaction;

      const transactionActionInsert: ITransactionActionInsert = {
        taz: registerUserTaz!,
        amount: newAmount,
        bankAccountNumber: newBankAccountNumber,
        transactionType: newTransactionType
      }

      await transactionsActionDispatch(insertTransaction({transactionData: transactionActionInsert,token:registerUserWebDataOnly!.token})).unwrap();

      if (newUpdateDialogsDataRef.current) {
        newUpdateDialogsDataRef.current(
          {
            titleSuccess: "הוספת פעולה צלחה ❇️",
            msgSuccess: "הפעולה בוצעה בהצלחה",
            btnSuccess: "Ok",
            actionSuccess: () => GetUserChoiceOnSuccess,
            showSuccess: true
          })
      }   
    }
    catch (err) {
      const errorString =
        err instanceof Error ? err.message :
          typeof err === "string" ? err : "An unknown error has occurred.";
      if (newUpdateDialogsDataRef.current) {
        newUpdateDialogsDataRef.current(
          {
            titleWarning: "שגיאת הוספת פעולה 📛",
            msgWarning: errorString,
            btnWarning: "Ok",
            actionWarning: () => GetUserChoiceOnError,
            showWarning: true
          })
      }
    }
  };

  return (
    <WebForm fields={fields} onSubmitAction={handleSubmit} title='הפקדה / משיכה חדשה' btnTitle="בצע" updateDialogsData={(fn) => newUpdateDialogsDataRef.current = fn} />
  )
}
