namespace Server.Helpers.Service
{
    using Microsoft.AspNetCore.DataProtection.KeyManagement;
    using Server.Helpers.ServiceInterfaces;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// The class responsible for AES action.
    /// </summary>
    public class SecurityService : ISecurityService
    {
        //We will define the key vector for the encryption

        private readonly string key;
        private readonly string iv;

        public SecurityService(string key, string iv)
        {
            this.key = key;
            this.iv = iv;
        }

        // Uses the AES algorithm for text encryption.
        // It involves encrypting a string (value)
        // using a key and an intermediate value (IV)
        // that is converted to base64.
        public string CreateEncryptorValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(value);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        //This code performs the opposite operation of encryption,
        //that is,
        //it decrypts the encrypted information(value) given to it.
        public string GetDecryptValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(value)))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
