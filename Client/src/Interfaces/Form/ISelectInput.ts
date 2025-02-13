import { typeOption } from "../../utils/GeneralVar";
import { IInputField } from "./IInputField";

// The interface responsible for Structure declaration
// for Select Input field
export interface ISelectInput extends IInputField {
  options:typeOption[];
}
