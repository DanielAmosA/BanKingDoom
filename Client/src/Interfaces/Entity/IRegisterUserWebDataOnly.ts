// The interface responsible for Structure declaration for RegisterUser 
// ( only the data necessary for using the action site ) entity
export interface IRegisterUserWebDataOnly  {
  id:number;
  taz: string;
  hebrewFullName: string;
  birthdayDate:Date;
  token:string;
}