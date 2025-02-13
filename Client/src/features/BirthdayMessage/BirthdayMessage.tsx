import './BirthdayMessage.scss';
import { isBirthday } from '../../utils/UserFun';
import { AppAuthData } from '../../App';
export const BirthdayMessage = () => {

  const { registerUserWebDataOnly } = AppAuthData();
  return (

    <>
      {/* Condition that checks if today is the user's birthday (isBirthday(user.birthdayDate)), 
            and if so, a magical, personalized birthday message will be displayed. */}

      {isBirthday(registerUserWebDataOnly!.birthdayDate) ? (
        <div className="birthdayMsg">
          {registerUserWebDataOnly!.hebrewFullName}, היום הוא יום קסום במיוחד – יום הולדתך! 🎉
          <br />
          אולי תרצה למשוך קצת מזל מהמערכת או להפקיד חיוך גדול בחייך? 💰
        </div>
      ) : null}
    </>
  )
}
