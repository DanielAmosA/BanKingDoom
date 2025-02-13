using Azure;
using Azure.Core;
using Server.Helpers.CustomException;
using Server.ModelsInterfaces.ExternalProvider;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Server.Models.ExternalProvider
{

    /// <summary>
    /// The class responsible for Used as a generic API client, 
    /// meaning it allows you to make API calls to any given address 
    /// with any request and receive a response in a generic format.
    /// </summary>

    public class ActionAPIClient : IAPIClient
    {
        private readonly HttpClient httpClient;

        public ActionAPIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TResponse> GoAPIClientAction<TResponse, TRequest>(string actionAPIUrl, TRequest actionAPIRequest, HttpMethod actionAPIMethod)
            where TResponse : IResponse
            where TRequest : IActionRequest
        {
            try
            {

                // Actual implementation would go here
                // This is a mock implementation
                return (TResponse)(IResponse)new ActionAPIResponse<string>
                {
                    Code = "200",
                    Data = "Success"
                };
            }
            catch (Exception ex)
            {
                throw new APIActionException($"API call exception occurred : {ex.Message}");
            }
        }
    }
}
