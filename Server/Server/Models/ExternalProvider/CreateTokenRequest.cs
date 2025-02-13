using Server.ModelsInterfaces.ExternalProvider;

namespace Server.Models.ExternalProvider
{
    /// <summary>
    /// The class responsible for Structure declaration for CreateToken 
    /// ( External provider - OpenBanking )  request parameters.
    /// </summary>
    public class CreateTokenRequest : IActionRequest, ITokenRequest
    {
        public string UserID { get; set; }
        public string SecretID { get; set; }

        public CreateTokenRequest()
        {
            UserID = string.Empty;
            SecretID = string.Empty;
        }
    }
}
