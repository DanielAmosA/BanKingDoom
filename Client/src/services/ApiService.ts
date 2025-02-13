import { IApiAction } from "../Interfaces/Api/IApiAction";
import { IApiResponse } from "../Interfaces/Api/IApiResponse";

//#region Members

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

//#endregion

//#region API Methods

export const CallApiAction = async ({
  controller,
  method,
  service,
  urlParams,
  body,
  token
}: IApiAction): Promise<IApiResponse> => {
  
  // Setting the Header
  const myHeaders = new Headers({ "Content-Type": "application/json" });
  if (token) {
    myHeaders.append("Authorization", `Bearer ${token}`);
  }

  // Setting the API address
  let apiActionUrl = `${API_BASE_URL}/${controller}`;
  if (service) apiActionUrl += `/${service}`;
  if (urlParams) apiActionUrl += `?${urlParams}`;

  // Setting parameters
  const actionParameters: RequestInit = {
    method,
    headers: myHeaders,
    redirect: "follow" as RequestRedirect,
  };

  // Adding the request body 
  // only if it is a request that requires a `body`
  if (body && method !== 'Get') {
    actionParameters.body = JSON.stringify(body);
  }

  try {
    // Makes an API call using fetch, and handles errors if the response from the server is invalid. 
    // If the response contains errors, the code parses the error details and returns a detailed error message. 
    // If everything is correct, the code returns the data received from the API. 
    // The overall goal is to make the call in an orderly manner, detect errors, 
    // and send clear error messages if necessary.
      
    const response = await fetch(apiActionUrl, actionParameters);
    if (!response.ok) {
      const errorData = await response.json().catch(() => null);

      if(errorData?.errors)
      {
        let logMessage = '';
        await Object.keys(errorData?.errors).forEach(key => {
          logMessage += `${key}: ${errorData?.errors[key].join(', ')}\n\n`;
        });
        throw new Error(logMessage)
      }

      throw new Error(errorData?.message || errorData?.error || response.statusText);
    }

    return { success: true, data: await response.json() };
  } catch (error) {
    return { success: false, error: error instanceof Error ? error.message : "An unknown error occurred" };
  }
};
