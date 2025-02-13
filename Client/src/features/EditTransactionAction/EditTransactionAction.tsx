import { useState } from 'react';
import './EditTransaction.scss';
import { TextInput } from '../../components/WebFormFields/TextInput';
import { validateField } from '../../utils/ValidFun';
import { IEditTransactionActionProps } from '../../Interfaces/Props/IEditTransactionActionProps';
import { updateTransactionAction } from '../../store/slices/transactionActionSlice';
import { useTransactionsActionDispatch } from '../../store/hooksTransactionsAction';
import { AppAuthData } from '../../App';

export const EditTransactionAction: React.FC<{ editTransactionActionProps: IEditTransactionActionProps }> = (
    {
        editTransactionActionProps
    }
) => {

    //#region Hooks
    const [editedTransactionAction, setEditedTransactionAction] = useState({ ...editTransactionActionProps.transactionAction });
    const [errors, setErrors] = useState<Record<string, string>>({});
    const [isSubmitting, setIsSubmitting] = useState(false);
    //#endregion

    //#region Store and provider
    const transactionsActionDispatch = useTransactionsActionDispatch();
    const { registerUserWebDataOnly } = AppAuthData();
    //#endregion

    //#region Handles

    // Running the validation function (validateField) 
    // to check if the new value is correct.

    // If there is an error, 
    // it is saved in a state called errors using setErrors.

    // The new value is saved 
    // in a state called editedTransaction 
    // which is used as the current data of the form.

    const handleChange = (name: string, value: string | number) => {
        const error = validateField(name, value);
        setErrors((prev) => ({ ...prev, [name]: error || "" }));
        setEditedTransactionAction((prev) => ({ ...prev, [name]: value }));
    };


    //   Performs a comprehensive check of the form 
    // and saves the data if there are no errors.

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {

        // stops the default form submission.
        e.preventDefault();

        const newErrors: Record<string, string> = {};

        // Object.keys always returns an array of strings (string[]).
        // In order for TypeScript to understand that these keys belong to an editedTransaction, 
        // we cast them using as Array<keyof typeof editedTransaction>.

        (Object.keys(editedTransactionAction) as Array<keyof typeof editedTransactionAction>)

            .forEach((objKey) => {

                // Performs an error check for each field based on its current value.
                // If there is an error, it is saved in newErrors.

                const error = validateField(objKey, editedTransactionAction[objKey] as string);
                if (error) newErrors[objKey] = error;
            }
            );

        // Updates all errors we found in the error state.
        // Saving the editedTransaction if there are no errors

        setErrors(newErrors);

        // Check if all errors are empty.
        // If so, pass the information saved in editedTransaction 
        // to the function that comes from store (saving the data)
        // And from the prop.

        if (Object.values(newErrors).every((valError) => !valError)) {
            setIsSubmitting(true);
            try {
                await transactionsActionDispatch(updateTransactionAction({transactionsAction:editedTransactionAction,taz:registerUserWebDataOnly!.taz,token:registerUserWebDataOnly!.token})).unwrap();
                editTransactionActionProps.onSave(editedTransactionAction);
            } catch (error) {
                const errorMessage = error instanceof Error ? error.message : 'שגיאה בעדכון הפעולה';
                setErrors(prev => ({ ...prev, submit: errorMessage }));
            } finally {
                setIsSubmitting(false);
            }
        }
    };

    //#endregion

    return (
        <div className="editTranDialog" >
            <div className={`editTran ${Object.values(errors).some(error => error !== '')
                && 'editTranErrors'}`}>
                <div className="editTranTop">
                    <h3 className="editTranTopTitle">עריכת פעולה קיימת</h3>

                    {/* displays a form with fields for updating a transaction in the pop. */}

                    <form className="editTranMain" onSubmit={handleSubmit}>
                        <TextInput
                            label="סכום"
                            name="amount"
                            value={editedTransactionAction.amount + ""}
                            onChange={handleChange}
                            error={errors.amount}
                            placeholder="הזן סכום"
                            type="number"
                        />
                        <TextInput
                            label="מספר חשבון"
                            name="bankAccountNumber"
                            value={editedTransactionAction.bankAccountNumber}
                            onChange={handleChange}
                            error={errors.bankAccountNumber}
                            maxLength={10}
                            placeholder="הזן מספר חשבון"
                            type="text"
                        />

                        {errors.submit && (
                            <div className="editTranError">
                                {errors.submit}
                            </div>
                        )}

                        <div className="editTranBtnsMain">
                            <button type="submit" className="editTranBtns editTranSaveBtn" disabled={isSubmitting} >{isSubmitting ? '...שומר' : '✅ שמור'}</button>
                            <button type="button" className="editTranBtns editTranCancelBtn" onClick={() => editTransactionActionProps.onCancel()}>
                                ❌ בטל
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}