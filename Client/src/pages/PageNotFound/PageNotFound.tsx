import { useNavigate } from "react-router-dom";
import "./PageNotFound.scss";
import pageNotFoundImg from "../../assets/images/site/pageNotFound.png";

export const PageNotFound = () => {
  const navigate = useNavigate();

  return (
      <div className="page pageNotFound">
          <h1 className="pageNotFoundTitle">404 - הדף יצא להרפתקה קסומה!</h1>
          <p className="pageNotFoundSubTitle">נראה שהדף שחיפשת הלך לחפש אוצרות בממלכת הנתונים. הוא כנראה עצר לדרינק עם המפתחים...</p>
          <p className="pageNotFoundSubTitle">אבל אל דאגה! אנחנו כאן כדי להחזיר אותך אל מסלול הקסם – לאן שתרצה, מתי שתרצה.</p>
          <button 
              className="pageNotFoundBtn" 
              onClick={() => navigate("/")}
          >
              חזרה לדף הראשי ✨
          </button>
          <img 
              src={pageNotFoundImg}
              alt="Page Not Found Img" 
              className="pageNotFoundImg"
          />
      </div>
  );
}
