import headerImg from "../../assets/images/site/logo.png";
import { RenderWebMenu } from "../Navigation/RenderNavigation"
import "./Header.scss"

// The header frame of the website.
export const Header  = () => {
    return (
        <div className="header">
      <div className="headerTop">
        <h2 className="headerTopTitle">הפניקס</h2>
      </div>
      <div className="headerCenter">
        <img
          className="headerCenterImg"
          src={headerImg}
          alt="Logo"
        />
      </div>
      <div className="headerBottom">
        <RenderWebMenu />
      </div>
    </div>
    )
}

