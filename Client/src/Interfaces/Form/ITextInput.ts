import { typeField } from "../../utils/GeneralVar";
import { IInputField } from "./IInputField";

// The interface responsible for Structure declaration
// for Text Input field
export interface ITextInput extends IInputField {
  type: typeField;
  maxLength?: number;
  placeholder?: string;
}
