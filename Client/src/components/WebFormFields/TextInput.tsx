import React from 'react'
import { ITextInput } from '../../Interfaces/Form/ITextInput'
import './WebFormFields.scss';

export const TextInput: React.FC<ITextInput> = (
    { 
        label, name, value, onChange, error, maxLength, placeholder, type = "text" 
    }
) => {

  return (
    <div className="webFormField">
    <label className='webFormFieldLabel'  htmlFor={name}>{label}</label>
    <input
      id={name}
      name={name}
      type={type}
      value={value}
      onChange={(e) => onChange(name, e.target.value)}
      maxLength={maxLength}
      placeholder={placeholder}
      className={`webFormFieldInput  ${error ? "webFormFieldInputInError" : ""}`}
    />
    {error && <div className="webFormFieldErrors">{error}</div>}
  </div>
  )
}
