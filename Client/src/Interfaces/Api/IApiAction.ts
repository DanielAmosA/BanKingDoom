import {typeController, typeMethod, typeService} from "../../utils/GeneralVar";

// The interface responsible for Structure declaration for ApiAction
export interface IApiAction {
    controller : typeController;
    method: typeMethod ;
    service?: typeService;
    urlParams?: URLSearchParams;
    body?: unknown;
    token?: string;
  }