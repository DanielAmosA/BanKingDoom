import { IRegisterUserBasic } from "./IRegisterUserBasic";

// The interface responsible for Structure declaration for RegisterUser entity
export interface IRegisterUser extends IRegisterUserBasic {
    hebrewFullName:string;
    englishFullName:string;
    birthdayDate:Date;
  }