namespace Server.Models.Settings
{
    /// <summary>
    /// The class responsible for Structure declaration for Jwt Settings
    /// </summary>
    public class JwtSettings
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryMinutes { get; set; }

    }
}
