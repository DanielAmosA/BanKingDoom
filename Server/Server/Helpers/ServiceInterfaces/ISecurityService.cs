namespace Server.Helpers.ServiceInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for SecurityService
    /// </summary>
    public interface ISecurityService
    {
        public string CreateEncryptorValue(string value);
        public string GetDecryptValue(string value);
    }
}
