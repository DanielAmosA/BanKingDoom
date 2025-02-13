/* eslint-disable no-irregular-whitespace */
import { useCallback, useEffect, useMemo, useState } from "react";
import { IFormProps } from "../../Interfaces/Form/IFormProps";
import './WebForm.scss';
import { IFormField } from "../../Interfaces/Form/IFormField";
import { ShowWarningDialog } from "../WebTools/ShowWarningDialog";
import { ShowSuccessDialog } from "../WebTools/ShowSuccessDialog";
import { dialogActions, kindButtons } from "../../utils/GeneralVar";
import { IDialogsData } from "../../Interfaces/Form/IDialogsData";

export const WebForm: React.FC<IFormProps> = ({ fields, onSubmitAction,
  title = 'טופס חדש', btnTitle = 'שלח', 
  imageSrc, imageAlt = 'form image' ,
  updateDialogsData }) => {


  //#region  Hook

  const [values, setValues] = useState<{ [key: string]: string }>({});
  const [errors, setErrors] = useState<{ [key: string]: string }>({});
  const [touched, setTouched] = useState<{ [key: string]: boolean }>({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [showPasswords, setShowPasswords] = useState<{ [key: string]: boolean }>({});

  const [titleSuccess, setTitleSuccess] = useState<string>('');
  const [msgSuccess, setMsgSuccess] = useState<string>('');
  const [actionSuccess, setActionSuccess] = useState<dialogActions>(() => {});
  const [btnSuccess, setBtnSuccess] = useState<kindButtons>('Ok');
  const [showSuccess, setShowSuccess] = useState<boolean>(false);

  const [titleWarning, setTitleWarning] = useState<string>('');
  const [msgWarning, setMsgWarning] = useState<string>('');
  const [actionWarning, setActionWarning] = useState<dialogActions>(() => {});
  const [btnWarning, setBtnWarning] = useState<kindButtons>('Ok');
  const [showWarning, setShowWarning] = useState<boolean>(false);

  // TUpdates dialog details on the screen, 
  // checking whether any field within a status object (which contains data such as title, message, button, etc.) 
  // is undefined. If the field is defined, 
  // it updates the state of the component with the new values ​​provided.

  const handleUpdateDialogsData = useCallback((status: IDialogsData) => {
    if (!status) return;
    if (status.titleSuccess !== undefined) setTitleSuccess(status.titleSuccess);
    if (status.msgSuccess !== undefined) setMsgSuccess(status.msgSuccess);
    if (status.actionSuccess !== undefined) setActionSuccess(status.actionSuccess);
    if (status.btnSuccess !== undefined) setBtnSuccess(status.btnSuccess);
    if (status.showSuccess !== undefined) setShowSuccess(status.showSuccess);

    if (status.titleWarning !== undefined) setTitleWarning(status.titleWarning);
    if (status.msgWarning !== undefined) setMsgWarning(status.msgWarning);
    if (status.actionWarning !== undefined) setActionWarning(status.actionWarning);
    if (status.btnWarning !== undefined) setBtnWarning(status.btnWarning);
    if (status.showWarning !== undefined) setShowWarning(status.showWarning);
}, []);

 useEffect(() => {
  if (updateDialogsData) {
    updateDialogsData(handleUpdateDialogsData);
  }
}, [updateDialogsData, handleUpdateDialogsData]);

  //#endregion

  //#region  Validations 


  // The code here uses useCallback to avoid reinitializing the function every time the component is updated, 
  // so the function will remain in the same memory and perform more efficiently.

  // The validateField function is 
  // a function that is executed with a callback on each field in the form. 
  // The function accepts two parameters: field (a field in the form) 
  // and value (the value entered in the field).
  const validateField = useCallback((field: IFormField, value: string): string | null => {

    // Required field check only if the field was touched
    if (field.required && touched[field.name] && !value.trim()) {
      return 'שדה חובה';
    }

    // Check password match
    if (field.confirmWith && values[field.confirmWith] !== value) {
      return 'הסיסמאות אינן תואמות';
    }

    // If the field has a list of validation functions (via field.validations), 
    // the function iterates over each one and performs the validation. 
    // If one of the functions returns an error, the function returns the error.

    if (field.validations) {
      for (const validation of field.validations) {
        const error = validation(value);
        if (error) return error;
      }
    }

    // If no error is found, the function returns null, 
    // indicating that there are no errors and the input is valid.

    return null;
  }, [values, touched]);


  // The code updates the values ​in the form, 
  // and also performs a password validation check 
  // if there is a password field in the form.

  // In other words, the code manages the updating of the values ​​of the form fields, 
  // and in the case of a password field, 
  // triggers the validation of the passwords when the value changes.

  const handleChange = useCallback((e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {

    // The function receives an event (e), which contains the field name (name) 
    // and the value entered into it (value).

    const { name, value } = e.target;

    // It then updates the values ​​state (setValues) with the new value entered into the corresponding field. 
    // The field name and value are updated in the state.

    setValues(prev => ({ ...prev, [name]: value }));

    // Marking the field as touched.
    if (!touched[name]) {
      setTouched(prev => ({ ...prev, [name]: true }));
    }

    // The function finds the field in the fields configuration based on the field name.
    // The field can be a password type or a field with validation 
    // if the confirmWith property is present).

    const field = fields.find(checkField => checkField.name === name);
    if (!field) return;

    // Real-time validate check
    const error = validateField(field, value);
    setErrors(prev => ({
      ...prev,
      [name]: error || ''
    }));

    // If the field is a password field (or a field that requires validation with another field), 
    // the validatePasswordMatch function will be called after the values ​​are updated.

    // If it's a password field, 
    // check the verification field as well.
    if (field.type === 'password') {
      const confirmField = fields.find(f => f.confirmWith === field.name);
      if (confirmField) {
        const confirmValue = values[confirmField.name] || '';
        const confirmError = validateField(confirmField, confirmValue);
        setErrors(prev => ({
          ...prev,
          [confirmField.name]: confirmError || ''
        }));
      }
    }

    // If it's a password verification field, 
    // check the original password field as well.
    if (field.confirmWith) {
      const mainField = fields.find(f => f.name === field.confirmWith);
      if (mainField) {
        const mainValue = values[mainField.name] || '';
        const mainError = validateField(mainField, mainValue);
        setErrors(prev => ({
          ...prev,
          [mainField.name]: mainError || ''
        }));
      }
    }

    // The function is defined with useCallback to prevent it from being recreated 
    // on every state change, 
    // which helps maintain better performance during multiple updates.

  }, [fields, values, touched, validateField]);


  // The code checks whether all required fields in the form are valid and validated, 
  // and returns a value indicating whether the form is valid or not.

  // The code here helps ensure that all required fields in the form have been filled out correctly by the user, 
  // and only in this case is the form considered valid (ready to be submitted).

  const isFormValid = useMemo(() => {

    // The function uses useMemo to prevent unnecessary recalculation of the result unless there is a change in one of the variables 
    // that consume the function (such as fields, values, touched, and validateField).

    // It starts by filtering out all fields 
    // that are defined as required (field.required) from the field list.

    const requiredFields = fields.filter(field => field.required);

    // The function checks that all required fields 
    // (found in requiredFields) are validated. 
    // It does this by using every to ensure that each required field is accepted if:

    // The field was entered by the user (checking if the field was touched – touched[field.name]).
    // The value entered in the field is not empty (after trimming spaces).
    // The value passes the validation done by validateField.

    const allFieldsValid = requiredFields.every(field => {
      const value = values[field.name] || '';
      return touched[field.name] && value.trim() && !validateField(field, value);
    });

    // If all required fields are valid and validated, 
    // the function returns true (all fields are valid). 
    // Otherwise, it returns false.

    return allFieldsValid;
  }, [fields, values, touched, validateField]);

  //#endregion

  //#region  Form Action

  // The code responsible for toggling the password display state 
  // (whether it is visible or hidden) 
  // in a specific field in the form.

  // The togglePasswordVisibility function allows you to dynamically show and hide passwords 
  // whenever a button is clicked that triggers the change in the specified password field.

  const togglePasswordVisibility = (fieldName: string) => {
    setShowPasswords(prev => ({

      // The function uses the previous state (prev) 
      // to update the current value (true or false) of the specified field.

      // If the value was true (the password is visible), 
      // the value will be updated to false (the password is hidden), 
      // and vice versa.

      // It uses the spread operator (...prev) to keep all other values 
      // ​​in the previous state and replace only the value of the specified field.
      ...prev,

      //       The name of the field (fieldName) is used as a key in the new state, a
      // nd the new value will be the inverse of the previous value (!prev[fieldName]), 
      // thus maintaining an updated state that describes whether the password is visible or not.

      [fieldName]: !prev[fieldName]
    }));
  };

  // The code you show here describes the handleBlur function 
  // that is invoked when a field in a form loses focus (the user has left the field).

  const handleBlur = (e: React.FocusEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    // The function receives an event e of type FocusEvent 
    // that contains the name and value of the field in which a change was made.

    const { name, value } = e.target;

    // The function updates the state of the fields that the user has touched (via setTouched). 
    // This state defines which fields have already been entered (blurred) by the user.

    // If the field was touched, it is updated to true in the "touched" array.

    // The code performs the following actions when the user moves from one field to another:

    // It updates the state of the fields that were touched.
    // It checks the value entered by the user and performs the validation of the field.
    // If there is an error, it is stored in the errors state.

    setTouched(prev => ({ ...prev, [name]: true }));

    // The function searches for the field in the fields list 
    // that matches the name of the field the user touched (name).

    const field = fields.find(checkField => checkField.name === name);

    // If the field is found, 
    // the function sends the value stored in the field 
    // to the validateField function to validate the entered value.

    if (field) {
      const error = validateField(field, value);

      // If an error is found, it updates the errors state (setErrors). 
      // If no error is found, it updates the field as '' (empty).
      setErrors(prev => ({
        ...prev,
        [name]: error || ''
      }));
    }
  };

  // The code you are showing here describes the handleSubmit function, 
  // whose job is to handle the submission of the form, check if all the fields are correct,
  // and then send the data to the server.

  // The code is managed asynchronously and ensures 
  // that the submission will only be made if the form is valid, 
  // and then clears the various fields and states after submission.

  const handleSubmit = async (e: React.FormEvent) => {

    // Preventing the default form submission
    // prevents the browser from automatically submitting the form, 
    // so that the function can perform the necessary actions before submitting.

    e.preventDefault();

    // Mark all fields as touched
    const allTouched: { [key: string]: boolean } = {};
    fields.forEach(field => {
      allTouched[field.name] = true;
    });
    setTouched(allTouched);

    // If the form is invalid (!isFormValid), 
    // the function returns immediately and stops the submission process.

    if (!isFormValid) return;

    // set IsSubmitting(true) updates the "Submitting" state 
    // to indicate that submission is in progress.

    setIsSubmitting(true);

    // Inside the try block, the function sends the data with await onSubmit(formData);
    //  meaning it waits for the result from the onSubmit function 
    // that performs the sending to the server (presumably).

    try {
      // A FormData object is created, which is a special object 
      // for submitting form data in the form of a key-value pair.

      const formData = new FormData();

      //  breaks the values ​​object into key-value pairs and appends them 
      // to the submitted formData object.

      Object.entries(values).forEach(([key, value]) => {
        formData.append(key, value);
      });

      // If the sending is successful, 
      // a reset is performed for the state fields (values, errors, hits).

      await onSubmitAction(formData);
      setValues({});
      setErrors({});
      setTouched({});
    }

    // If there were any problems submitting, 
    // the function will catch the error

    catch (err) {
      const errorString =
        err instanceof Error ? err.message :
          typeof err === "string" ? err : "An unknown error has occurred.";
      setMsgWarning(errorString);
      setMsgSuccess(errorString);
      setShowSuccess(true);
    }

    // The finally block updates the setIsSubmitting(false) state 
    // to stop displaying the submission status.

    finally {
      setIsSubmitting(false);
    }
  };

  //#endregion

  //#region Actions

  //#endregion


  return (
    <div className="webForm">
      <h2 className="webFormTop">{title}</h2>
      <form className="webFormMain" onSubmit={handleSubmit} noValidate>

        {/* Uses fields.map to map all the fields submitted to the form. */}

        {fields.map(field => (
          <div className="webFormField" key={field.name}>
            <label className="webFormFieldLabel">
              {field.required && touched[field.name] && <span className="required">*</span>}
              {field.label}
            </label>

            {/* For each field, a <textarea> element is created if the field type is textarea, 
            or an <input> element if the field is another type (such as password, plain text). */}
            <div className="webFormFieldCenter">
              {
                field.type === 'select' ? (
                  <select
                    name={field.name}
                    value={values[field.name] || ''}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    className={`webFormFieldBasic ${touched[field.name] && errors[field.name] ? 'webFormFieldInError' : 'webFormFieldNoError'}`}
                    disabled={isSubmitting}
                  >
                    <option value="" disabled>בחר אפשרות</option>
                    {field.options?.map(option => (
                      <option key={option.value} value={option.value}>
                        {option.label}
                      </option>
                    ))}
                  </select>
                ) :

                  field.type === 'textarea' ? (
                    <textarea
                      name={field.name}
                      value={values[field.name] || ''}
                      onChange={handleChange}
                      onBlur={handleBlur}
                      className={`webFormFieldBasic ${touched[field.name] && errors[field.name] ? 'webFormFieldInError' : 'webFormFieldNoError'}`}
                      placeholder={field.placeholder}
                      disabled={isSubmitting}
                    />
                  ) : (
                    <>
                      <input
                        type={field.type === 'password' && showPasswords[field.name] ? 'text' : field.type}
                        name={field.name}
                        value={values[field.name] || ''}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        className={`webFormFieldBasic ${touched[field.name] && errors[field.name] ? 'webFormFieldInError' : 'webFormFieldNoError'} 
                              ${field.type === 'password' ? 'webFormFieldPassword' : ''}`}
                        placeholder={field.placeholder}
                        disabled={isSubmitting}
                        autoComplete="webPassword"
                      />
                      {field.type === 'password' && (
                        <button
                          type="button"
                          className="webFormFieldPasswordToggle"
                          onClick={() => togglePasswordVisibility(field.name)}
                          tabIndex={-1}
                        >
                          {showPasswords[field.name] ? (
                            <span className="webFormFieldPasswordToggleIcon">🔲</span>
                          ) : (
                            <span className="webFormFieldPasswordToggleIcon">🔳</span>
                          )}
                        </button>
                      )}
                    </>
                  )}
            </div>

            {/* If there is an error in a field, 
            it is displayed below the field using a <div> element 
            that displays the error text. */}

            {touched[field.name] && errors[field.name] && (
              <div className="webFormFieldError">
                <div className="webFormFieldErrorDesc">
                  {errors[field.name]}
                </div>
              </div>
            )}
          </div>
        ))}

        {/* Showing specifies a dynamic submit button that updates 
        based on the state of the form. 
        The button ensures that it is not clicked if the form is invalid
        or if the function is already submitting the information. */}
        <button
          type="submit"
          disabled={!isFormValid || isSubmitting}
          className={`webFormBtn ${!isFormValid ? 'webFormBtnInDisabled' : ''} ${isSubmitting ? 'webFormBtnInLoading' : ''}`}
        >
          {isSubmitting ? 'נשלח ...' : btnTitle}
        </button>

        {imageSrc && (
          <div className="webFormBottom">
            <img className="webFormImg" src={imageSrc} alt={imageAlt} />
          </div>
        )}
      </form>

      {
        showWarning &&
        (
          ShowWarningDialog(titleWarning,
            <>{msgWarning}</>,
            actionWarning,
            btnSuccess
          )
        )
      }

      {
        showSuccess &&
        (
          ShowSuccessDialog(titleSuccess,
            <>{msgSuccess}</>,
            actionSuccess,
            btnWarning
          )
        )
      }
    </div>
  );
};
