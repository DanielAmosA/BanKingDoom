import { useRef } from 'react';
import { WebForm } from '../../components/WebForm/WebForm';
import { IFormField } from '../../Interfaces/Form/IFormField';
import { validateEnglishFullName, validateHebrewFullName, validateStrongPassword } from '../../utils/GeneralVar';
import { validateDateNotAfterToday, validateTaz } from '../../utils/ValidFun';
import './SignUp.scss';
import { IDialogsData } from '../../Interfaces/Form/IDialogsData';
import { useNavigate } from 'react-router-dom';
import { IRegisterUser } from '../../Interfaces/Entity/IRegisterUser';
import { CallApiAction } from '../../services/ApiService';

export const SignUp = () => {

  const newUpdateDialogsDataRef = useRef<((status: IDialogsData) => void) | null>(null);
  const navigate = useNavigate();

  const fields: IFormField[] = [
    {
      name: 'hebrewFullName',
      label: 'שם מלא בעברית',
      type: 'text',
      required: true,
      validations: [
        validateHebrewFullName
      ],
      placeholder: 'הכנס שם מלא'
    },
    {
      name: 'englishFullName',
      label: 'שם מלא באנגלית',
      type: 'text',
      required: true,
      validations: [
        validateEnglishFullName
      ],
      placeholder: 'הכנס שם מלא באנגלית'
    },

    {
      name: 'birthdayDate',
      label: 'תאריך לידה',
      type: "date",
      required: true,
      validations: [validateDateNotAfterToday],
    },

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

  //#region  Dialog Action

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
    navigate("/signIn");
  }

  //#endregion

  const handleSubmit = async (formData: FormData): Promise<void> => {

    try {
      const createdAt = new Date();
      const taz = formData.get("taz") as string;
      const password = formData.get("password") as string;
      const birthdayDate = formData.get("birthdayDate") as unknown as Date;
      const englishFullName = formData.get("englishFullName") as string;
      const hebrewFullName = formData.get("hebrewFullName") as string;


      const registerUser: IRegisterUser = {
        createdAt: createdAt,
        taz: taz,
        password: password,
        birthdayDate: birthdayDate,
        englishFullName: englishFullName,
        hebrewFullName: hebrewFullName
      }

      const resCallApiAction = await CallApiAction({
        controller: 'RegisterUser',
        method: 'Post',
        service: 'RegisterUserInsert',
        body: registerUser,
      });

      if (resCallApiAction) {
        if (resCallApiAction.success) {
          if (newUpdateDialogsDataRef.current) {
            newUpdateDialogsDataRef.current(
              {
                titleSuccess: "הרשמה צלחה ❇️",
                msgSuccess: "ההרשמה בוצעה בהצלחה",
                btnSuccess: "Ok",
                actionSuccess: () => GetUserChoiceOnSuccess,
                showSuccess: true
              })
          }
        }
        else {
          if (newUpdateDialogsDataRef.current) {
            newUpdateDialogsDataRef.current(
              {
                titleWarning: "אזהרה בהרשמה 📛",
                msgWarning: resCallApiAction.error,
                btnWarning: "Ok",
                actionWarning: () => GetUserChoiceOnError,
                showWarning: true
              })
          }
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
            titleWarning: "שגיאת הרשמה 📛",
            msgWarning: errorString,
            btnWarning: "Ok",
            actionWarning: () => GetUserChoiceOnError,
            showWarning: true
          })
      }
    }

  };

  return (
    <WebForm fields={fields} onSubmitAction={handleSubmit} updateDialogsData={(fn) => newUpdateDialogsDataRef.current = fn} title='צור חשבון' btnTitle="הרשם" />
  )
}
