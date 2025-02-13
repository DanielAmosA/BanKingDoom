import { IBasic } from "./IBasic";

// The interface responsible for Structure declaration for RegisterUser ( Basic data ) entity
export interface IRegisterUserBasic extends IBasic {
  taz: string;
  password: string;
}
