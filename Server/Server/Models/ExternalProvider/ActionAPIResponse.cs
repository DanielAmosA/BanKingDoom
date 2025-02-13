using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Models.ExternalProvider
{
    /// <summary>
    /// The class responsible for Structure declaration for APIResponse
    /// </summary>
    public class ActionAPIResponse<TResponse> : BaseResponse, IAPIResponse<TResponse>
    {
        public TResponse Data { get; set; }
    }
}
