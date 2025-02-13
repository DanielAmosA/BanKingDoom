import { INavigationPart } from '../../Interfaces/Structure/INavigationPart';
import { Dashboard } from '../../pages/Dashboard/Dashboard';
import { Home } from '../../pages/Home/Home';
import { PageNotFound } from '../../pages/PageNotFound/PageNotFound';
import { SignIn } from '../../pages/SignIn/SignIn';
import { SignUp } from '../../pages/SignUp/SignUp';
import { TransactionActionHub } from '../../pages/TransactionHub/TransactionActionHub';

// Array of routes

export const NavWeb : INavigationPart[] = [
        {path: "/", name:"עמוד הבית",element:<Home/>,isNeedAuth:false,isInMenu:true},
        {path: "/signIn", name:"התחבר",element:<SignIn/>,isNeedAuth:false,isInMenu:true},
        {path: "/signUp", name:"הרשם",element:<SignUp/>,isNeedAuth:false,isInMenu:true},
        
        //Register User Section
        {path: "/", name:"התנתק",element:<Home/>,isNeedAuth:true,isInMenu:true},
        {path: "/dashboard", name:"אזור אישי",element:<Dashboard/>,isNeedAuth:true,isInMenu:true},
        {path: "/transactionHub", name:"פעולות",element:<TransactionActionHub/>,isNeedAuth:true,isInMenu:true},      

        // Other Section
        {path: "*", name:"עמוד לא נמצא",element:<PageNotFound/>,isNeedAuth:false,isInMenu:false},
    ]