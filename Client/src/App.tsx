import { createContext, useContext, useState } from 'react';
import './App.css'
import { Footer } from './components/Footer/Footer'
import { IAuthContextType } from './Interfaces/Structure/IAuthContextType';
import { BrowserRouter } from 'react-router-dom';
import { Header } from './components/Header/Header';
import { RenderWebRoutes } from './components/Navigation/RenderNavigation';
import { CallApiAction } from './services/ApiService';
import { IRegisterUser } from './Interfaces/Entity/IRegisterUser';
import { typeRole } from './utils/GeneralVar';
import { ITokenResponse } from './Interfaces/Api/ITokenResponse';
import { IRegisterUserWebDataOnly } from './Interfaces/Entity/IRegisterUserWebDataOnly';
import { IRegisterUserBasic } from './Interfaces/Entity/IRegisterUserBasic';

import { Provider } from 'react-redux';
import { storeTransactionsAction } from './store/storeTransactionsAction';
//#region Context

// Creating Default values
const AuthContext = createContext<IAuthContextType>({
  registerUserWebDataOnly: null,
  signInAction: async () => {
    return null;
  },
  signOutAction: () => { },
});

// Passing Data Deeply with Context
export const AppAuthData = () => useContext(AuthContext);

//#endregion

export const App = () => {

  //#region  Hook and Members

  // Hook to registerUser Details
  const [registerUserWebDataOnly, setRegisterUserWebDataOnly] = useState<IRegisterUserWebDataOnly | null>(null);

  //#endregion

  //#region  Methods

  // Get token 
  const GenerateToken = async (registerUser: IRegisterUser): Promise<void> => {

    const roleValue: typeRole = 'User';

    const queryParams = new URLSearchParams({
      role: roleValue,
    });

    const resCallApiAction = await CallApiAction({
      controller: 'Auth',
      method: 'Get',
      service: 'GenerateToken',
      urlParams: queryParams,
    });

    const resData = resCallApiAction.data as ITokenResponse;
    if (resData.success) {
      setRegisterUserWebDataOnly({
        id: registerUser.id!,
        taz: registerUser.taz,
        hebrewFullName: registerUser.hebrewFullName,
        birthdayDate: registerUser.birthdayDate,
        token: resData.token,
      })
    }
    else {
      throw new Error("Token generation failed");
    }
  };

  const signInAction = async (taz: string, password: string): Promise<string | null> => {

    // Make a call to the authentication API to check the taz
    try {
      const registerUserBasic : IRegisterUserBasic = {
        taz:taz,
        password: password
      }
      const resCallApiAction = await CallApiAction({
        controller: 'RegisterUser',
        method: 'Post',
        service: 'RegisterUserGetUserByTaz',
        body: registerUserBasic,
      });

      if (resCallApiAction) {
        if (resCallApiAction.success) {
          const registerUser: IRegisterUser = resCallApiAction.data as IRegisterUser;
          GenerateToken(registerUser);
          return null;
        }

        else {
          return resCallApiAction.error!;
        }
      }
      return null;

    }

    catch (err) {
      return `Login failed: Maybe Server problem ... - ${err}`;
    }
  };

  const signOutAction = (): void => {
    setRegisterUserWebDataOnly(null);
  };


  //#endregion

  return (
    <div className="App">
      {/* Wrap the app in the Redux Provider for transaction action */}
      <Provider store={storeTransactionsAction}> 
      {/* Provider for user */}
      <AuthContext.Provider value={{ registerUserWebDataOnly, signInAction, signOutAction }}>
        <BrowserRouter>
          <Header />
          {/* Main web data */}
          <RenderWebRoutes />
          <Footer />
        </BrowserRouter>
      </AuthContext.Provider>
      </Provider>

    </div>
  )
}
