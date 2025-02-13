using Server.ModelsInterfaces.Settings;

namespace Server.Models.Settings
{
    /// <summary>
    /// The class responsible for Structure declaration for OpenBanking (External Provider) Parameters
    /// </summary>
    public class DbConfig
    {
        public string ConnectionString { get; set; }

        public DbConfig()
        {
            ConnectionString = string.Empty;
        }
    }
}
