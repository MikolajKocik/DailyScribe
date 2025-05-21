using DailyScribe.Cryptography.AES.Interfaces;
using System.Security.Cryptography;

namespace DailyScribe.Cryptography.AES
{
    public class AES_decryption : IAES_decryption
    {
       public string Decrypt(string cipherText, byte[] masterKey)
        {
            try
            {
                byte[] cipherData = Convert.FromBase64String(cipherText);

                if (cipherData.Length < Program.iVSize)
                    throw new InvalidOperationException("Invalid cipher text format");

                byte[] iv = new byte[Program.iVSize];
                byte[] encryptedData = new byte[cipherData.Length - Program.iVSize];

                Buffer.BlockCopy(cipherData, 0, iv, 0, Program.iVSize); // copying the 16 bytes
                Buffer.BlockCopy(cipherData, Program.iVSize, encryptedData, 0, encryptedData.Length); // copying the rest bytes

                using var aes = Aes.Create();
                aes.Mode = CipherMode.CBC; // encrypting mode, where every block depends on block before
                aes.Padding = PaddingMode.PKCS7; // adding some bytes to '16' if needed
                aes.Key = masterKey;
                aes.IV = iv;

                using MemoryStream memoryStream = new(encryptedData);
                using var decrypter = aes.CreateDecryptor();
                using CryptoStream cryptoStream = new(memoryStream, decrypter, CryptoStreamMode.Read);
                using StreamReader streamReader = new(cryptoStream);

                return streamReader.ReadToEnd();
            }
             catch (CryptographicException ex)
            {
                throw new InvalidOperationException("Decryption failed", ex);
            }
        }
    }
}
