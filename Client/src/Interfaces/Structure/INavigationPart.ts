import { JSX } from "react";

// The interface responsible for Structure declaration
// for NavigationPart
export interface INavigationPart {
    path: string;
    name: string;
    element: JSX.Element;
    isInMenu: boolean;
    isNeedAuth:boolean;
  }
  