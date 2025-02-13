import { Route, Routes, NavLink } from "react-router-dom";
import { NavWeb } from "./Navigation";
import { AppAuthData } from "../../App";
import { INavigationPart } from "../../Interfaces/Structure/INavigationPart";
import './RenderNavigation.scss'
import { JSX } from "react";

// Create the web routes
export const RenderWebRoutes = () => {

  // Call to  register user details
  const { registerUserWebDataOnly } = AppAuthData();

  return (
    <Routes>
      {NavWeb.map((item: INavigationPart, ind: number): JSX.Element | null => {

        // Get only basic roots 
        if (!item.isNeedAuth) {
          return <Route key={ind} path={item.path} element={item.element} />;
        }

        else {

          // For the logged-in user, we will check if they have permissions to access all the paths.
          if (registerUserWebDataOnly) {
            if (item.isNeedAuth) {
              return <Route key={ind} path={item.path} element={item.element} />;
            }
          }
        }
        return null;
      })}
    </Routes>

  );
};

// Create the web links
export const RenderWebMenu = () => {

  // Call to register user details and logout
  const { registerUserWebDataOnly, signOutAction } = AppAuthData();

  // Create NavLink according to item
  const MenuItem = ({ item }: { item: INavigationPart }): JSX.Element => {
    return (
      <div key={item.path} className="menuItem">
        {item.name === "התנתק" ? (
          <NavLink to={item.path} onClick={() => signOutAction()}>
            {item.name}
          </NavLink>
        ) : (
          <NavLink to={item.path}>{item.name}</NavLink>
        )}
      </div>
    );
  };

  // Create Nav links  with MenuItem according to NavWeb item properties
  return (
    <div className="menu">
      {
        NavWeb.map((navElem: INavigationPart, ind: number) => {

          // First, we will check if it is a path that should appear in the menu links and not a general link, 
          // such as a "Page Not Found" page.
          if (navElem.isInMenu) {

            // When logged in, we don't want links to the pages 
            // [Log in, Sign Up, Home Page] to be available.
            if (registerUserWebDataOnly) {
              if ((navElem.name === "התחבר") ||
                (navElem.name === "הרשם") ||
                (navElem.name === "עמוד הבית")) {
                return null
              }

              return <MenuItem key={ind} item={navElem} />;
            }

            // We want to ensure that general paths are displayed to users who are not logged in.
            else if (navElem.isNeedAuth) {
              return null;
            }

            return <MenuItem key={ind} item={navElem} />;
          }

        })}

    </div>
  );
};
