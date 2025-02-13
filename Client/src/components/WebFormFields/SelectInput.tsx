import React from 'react'
import { ISelectInput } from '../../Interfaces/Form/ISelectInput';
import './WebFormFields.scss';

export const SelectInput : React.FC<ISelectInput> = (
    { 
        label, 
        name, 
        value, 
        onChange, 
        error, 
        options 
    }
) => {
    return (
        <div className="webFormField">
          <label className='webFormFieldLabel' htmlFor={name}>{label}</label>
          <select
            id={name}
            name={name}
            value={value}
            onChange={(e) => onChange(name, e.target.value)}
            className={`webFormFieldInput  ${error ? "webFormFieldInputInError" : ""}`}
          >
            {options.map((option, index) => (
              <option key={index} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
          {error && <div className="webFormFieldErrors">{error}</div>}
        </div>
      );
}
