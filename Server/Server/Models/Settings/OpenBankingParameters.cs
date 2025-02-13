using Server.ModelsInterfaces.Settings;

namespace Server.Models.Settings
{
    /// <summary>
    /// The class responsible for Structure declaration for OpenBanking (External Provider) Parameters
    /// </summary>
    public class OpenBankingParameters
    {
        public string SecretId { get; set; }

        public OpenBankingParameters()
        {
            SecretId = string.Empty;
        }
    }
}
