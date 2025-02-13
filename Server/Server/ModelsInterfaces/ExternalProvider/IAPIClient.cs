using Server.Models;

namespace Server.ModelsInterfaces.ExternalProvider
{
    /// <summary>
    /// The interface responsible for Structure declaration for APIClient
    /// </summary>
    public interface IAPIClient
    {
        Task<TResponse> GoAPIClientAction<TResponse, TRequest>(string actionAPIUrl, TRequest actionAPIRequest, HttpMethod actionAPIMethod)
        where TResponse : IResponse
        where TRequest : IActionRequest;
    }
}
