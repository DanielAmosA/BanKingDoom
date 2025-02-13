using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Models.ExternalProvider
{
    /// <summary>
    /// The class responsible for Structure declaration for API Base Response
    /// </summary>
    public class BaseResponse : IBaseResponse, IResponse
    {
        public string Code { get; set; }
        public string? Message { get; set; }

        public BaseResponse()
        {
            Code = string.Empty;
        }
    }
}
