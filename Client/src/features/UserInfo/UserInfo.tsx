import React from 'react'
import avatarImg from "../../assets/images/user/avatar.png";
import './UserInfo.scss';
import { AppAuthData } from '../../App';

export const UserInfo: React.FC = () => {

  const { registerUserWebDataOnly } = AppAuthData();

  return (

    // display information about the user with text 
    // and image (avatar) formatting in a humorous and magical way.

    <div className="userInf">
      <img src={avatarImg} alt="avatar" className="userInfImg" />
      <h2 className="userInfTitle">
        ברוך הבא <span className="userInfSubTitle">{registerUserWebDataOnly?.hebrewFullName}</span>! <br />
      </h2>
    </div>
  )
}
