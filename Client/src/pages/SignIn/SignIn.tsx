import './SignIn.scss';
import { WebForm } from '../../components/WebForm/WebForm';
import { IFormField } from '../../Interfaces/Form/IFormField';
import { validateTaz } from '../../utils/ValidFun';
import { validateStrongPassword } from '../../utils/GeneralVar';
import { AppAuthData } from '../../App';
import { useNavigate } from 'react-router-dom';
import { useRef } from 'react';
import { IDialogsData } from '../../Interfaces/Form/IDialogsData';

export const SignIn = () => {

  const { signInAction } = AppAuthData();
  const newUpdateDialogsDataRef = useRef<((status: IDialogsData) => void) | null>(null);
  const navigate = useNavigate();

  const fields: IFormField[] = [
    {
      name: 'taz',
      label: 'תעודת זהות',
      type: 'text',
      required: true,
      validations: [validateTaz("תעודת זהות אינה תקינה, אנא בדוק שוב")],
      placeholder: 'הכנס תעודת זהות'
    },

    {
      name: 'password',
      label: 'סיסמה',
      type: 'password',
      required: true,
      validations: [
        validateStrongPassword
      ],
      placeholder: 'הכנס סיסמה'
    },

    {
      name: 'confirmPassword',
      label: 'אימות סיסמה',
      type: 'password',
      required: true,
      confirmWith: 'password',
      placeholder: 'הכנס אימות סיסמה'
    },

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
    navigate("/Dashboard");
  }

  const handleSubmit = async (formData: FormData): Promise<void> => {
    try {
      const taz = formData.get("taz") as string;
      const password = formData.get("password") as string;

      const resSignInAction = await signInAction(taz, password);
      if (resSignInAction) {
        if (newUpdateDialogsDataRef.current) {
          newUpdateDialogsDataRef.current(
            {
              titleWarning: "אזהרת התחברות ⚠️",
              msgWarning: resSignInAction,
              btnWarning: "Ok",
              actionWarning: () => GetUserChoiceOnError,
              showWarning: true
            })
        }
      }
      else {
        if (newUpdateDialogsDataRef.current) {
          newUpdateDialogsDataRef.current(
            {
              titleSuccess: "התחברות צלחה ❇️",
              msgSuccess: "ההתחברות בוצעה בהצלחה",
              btnSuccess: "Ok",
              actionSuccess: () => GetUserChoiceOnSuccess,
              showSuccess: true
            })
        }
      }
    }
    catch (err) {
      const errorString =
        err instanceof Error ? err.message :
          typeof err === "string" ? err : "An unknown error has occurred.";
      if (newUpdateDialogsDataRef.current) {
        newUpdateDialogsDataRef.current(
          {
            titleWarning: "שגיאת התחברות 📛",
            msgWarning: errorString,
            btnWarning: "Ok",
            actionWarning: () => GetUserChoiceOnError,
            showWarning: true
          })
      }
    }
  };


  return (
    <div className='page'>
      <WebForm fields={fields} onSubmitAction={handleSubmit} title='כניסה לחשבונך' btnTitle="התחבר" updateDialogsData={(fn) => newUpdateDialogsDataRef.current = fn} />
    </div>
  )
}
