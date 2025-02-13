// The interface responsible for Structure declaration
// for Input field
export interface IInputField {
    name: string;
    label: string;
    value:string, 
    onChange: (name: string, value: string | number) => void,
    error:string, 
  
  }