import { useEffect, useState } from 'react';
import './Home.scss';
import homeImg from "../../assets/images/site/home.png";
import securityImg from "../../assets/images/site/security.png";
import supportImg from "../../assets/images/site/support.png";
import { useNavigate } from "react-router-dom";

export const Home = () => {
  
  //Every 3 seconds, 
  // the sentence displayed on the screen changes 
  // according to index (indxSentence).
  // The first sentence starts with indxSentence = 0, 
  // and after 3 seconds, the second sentence will be displayed, 
  // and so on.

  const [indxSentence, setIndxSentence] = useState(0);
  const nav = useNavigate();
  const sentences = [
    "הפקדת כספים בצורה מהירה",
    "משיכות מהירות בלחיצת כפתור",
    "עבודה עם ספקים חיצוניים אמינים",
    "שירות מהיר ואיכותי 24/7"
  ];

  //A timer is defined that runs the code every 3 seconds (3000 milliseconds).
  // Each time it is run, 
  // the function changes indxSentence 
  // to the next sentence in the array (prev + 1).

  useEffect(() => {
    const changeSentencesTimer = setInterval(() => {

     //The use of % sentences.length causes the end of the list 
      // to return to the first sentence (circularity).

      setIndxSentence((prev) => (prev + 1) % sentences.length);
    }, 3000);

      //Clearing a timer (clearInterval) Once the component is unmounted, 
      // the timer is canceled to prevent unnecessary running of the function.

    return () => clearInterval(changeSentencesTimer);

    //Dependency:The mechanism will only be reactivated 
    // if the number of sentences in sentences.length changes.
    
  }, [sentences.length]);

  return (
    <div className="page home">
    <header className="homeHeader">
      <div className="homeHeaderCarousel">
        {       
        sentences.map((text, index) => (
          <div
            key={index}

            //The state (indxSentence) is updated every 3 seconds.
            // The loop adds the active class only to the current sentence 
            // (whose index is equal to indxSentence).
            // Using CSS, the active class can apply any other visual effect to the active sentence.

            className={`sentence ${index === indxSentence ? "active" : ""}`}
          >
            {text}
          </div>
        ))}
      </div>
    </header>
    <main className="homeCenter">
      <section className="homeCenterSec">
        <h2>הפקדת כספים</h2>
        <p>הפקידו כספים בקלות ובביטחון מלא.</p>
        <button className="homeCenterSecBtn" onClick={() => nav("/signIn")} >להפקדה</button>
      </section>
      <section className="homeCenterSec">
        <h2>משיכת כספים</h2>
        <p>משכו כספים במהירות וללא עיכובים.</p>
        <button className="homeCenterSecBtn" onClick={() => nav("/signIn")}>למשיכה</button>
      </section>
      <section className="homeCenterSec">
        <h2>עבודה עם ספקים חיצוניים אמינים</h2>
        <p>אנו דואגים לעשות כל פעולה בשיתוף עם ספקים חיצוניים אמיניים ומובילים.</p>
        <img className="homeCenterSecImg"  src={securityImg} alt="אבטחת עסקאות"/>
      </section>
      <section className="homeCenterSec">
        <h2>התאמה לכל מכשיר</h2>
        <p>כל פעולה תוכלו לבצע בקלות מכל מכשיר ממחשב ולפלאפון הנייד.</p>
        <img className="homeCenterSecImg"  src={supportImg} alt="תמיכה 24/7" />
      </section>
      <section className="homeCenterSec">
        <h2>הצטרפו אלינו!</h2>
        <p>
          נמאס לכם ממערכות משעממות? הגיע הזמן לשדרג את חוויית הכסף שלכם! הצטרפו
          אלינו ותהפכו את העולם הפיננסי לקסום, מרהיב ובעיקר רווחי. כי אצלנו,
          כסף זה לא רק מספרים - זה הרפתקה.
        </p>
        <button className="homeCenterSecBtn" onClick={() => nav("/signUp")}>להרשמה</button>
      </section>
    </main>
    <footer className="homeBottom">
      <img className="homeBottomImg" src={homeImg} alt="תמונה לסיום"  />
    </footer>
  </div>
  )
}
