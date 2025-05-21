using DailyScribe.Cryptography.AES.Interfaces;
using System.Security.Cryptography;

namespace DailyScribe.Cryptography.AES
{
    public class AES_encryption : IAES_encryption
    {
        public string Encrypt(string plaintext, byte[] masterKey)
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = masterKey;
            aes.IV = RandomNumberGenerator.GetBytes(Program.iVSize);

            using var memoryStream = new MemoryStream();
            memoryStream.Write(aes.IV, 0, Program.iVSize);

            using (var encryptor = aes.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(plaintext);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
