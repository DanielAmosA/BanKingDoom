import { typeKindsLoadData } from "../../utils/GeneralVar"
import loadingImg from '../../assets/images/site/loading.gif';
import loadingErrorImg from '../../assets/images/site/loadingError.gif';
import noDataImg from '../../assets/images/site/noData.gif';
import './ShowKindLoadData.scss'
import { JSX } from "react";

// Displaying an details about the data being loaded / received / failed
export const ShowKindLoadData = (kind: typeKindsLoadData): JSX.Element => {
    if (kind === "Wait") {
        return (
          <div className="mainKindLoadData">
            <div className="mainKindLoadDataMain">
              <div className="mainKindLoadDataCenter">
                <p className="mainKindLoadDataTitle waitTitle">טוען נתונים ...</p>
                <img className="mainKindLoadDataImg" src={loadingImg} alt="waitImg" />
              </div>
            </div>
          </div>
        );
      } 

      else if( kind === "NotFound"){
        return (
          <div className="mainKindLoadData">
            <div className="mainKindLoadDataMain">
              <div className="mainKindLoadDataCenter">
                <p className="mainKindLoadDataTitle waitTitle"> לא נמצאו נתונים להצגה 🫥</p>
                <img className="mainKindLoadDataImg" src={noDataImg} alt="waitImg" />
              </div>
            </div>
          </div>
        );
      }
      else {
        return (
          <div className="mainKindLoadData">
            <div className="mainKindLoadDataMain">
              <div className="mainKindLoadDataCenter">
                <p className="mainKindLoadDataTitle errorTitle">שגיאה בקבלת נתונים !</p>
                <img className="mainKindLoadDataImg" src={loadingErrorImg} alt="errorLoadImg" />
              </div>
            </div>
          </div>
        );
      } 
  
  }
  