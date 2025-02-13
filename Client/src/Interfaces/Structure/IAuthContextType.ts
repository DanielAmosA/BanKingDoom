import { IRegisterUserWebDataOnly } from "../Entity/IRegisterUserWebDataOnly";

// The interface responsible for Structure declaration
// for AuthContextType
export interface IAuthContextType {
  registerUserWebDataOnly: IRegisterUserWebDataOnly | null;
    signInAction: (taz: string, password: string) => Promise<string  | null>;
    signOutAction: () => void;
  }
  