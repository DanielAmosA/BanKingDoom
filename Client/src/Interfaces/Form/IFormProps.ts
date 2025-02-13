

// The interface responsible for Structure declaration

import { IDialogsData } from "./IDialogsData";
import { IFormField } from "./IFormField";

// for Form Props - WebForm
export interface IFormProps {
  fields: IFormField[];
  onSubmitAction: (data: FormData) => Promise<void>;
  title?: string;
  btnTitle?: string;
  imageSrc?: string;
  imageAlt?: string;
  updateDialogsData: (fn: (status: IDialogsData) => void) => void
  
}
