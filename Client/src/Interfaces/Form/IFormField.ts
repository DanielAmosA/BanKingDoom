import { typeField, typeOption, typeValidRule } from "../../utils/GeneralVar";

// The interface responsible for Structure declaration 
// for Form Field
export interface IFormField {
    name: string;
    label: string;
    type: typeField;
    required?: boolean;
    validations?: typeValidRule[];
    placeholder?: string;
    confirmWith?: string;
    options?: typeOption[]; 
  }
  