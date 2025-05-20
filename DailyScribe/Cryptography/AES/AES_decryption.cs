using System.Security.Cryptography;

namespace DailyScribe.Cryptography.AES
{
    public static class AES_decryption
    {
       public static string Decrypt(string cipherText, byte[] masterKey)
        {
            try
            {
                byte[] cipherData = Convert.FromBase64String(cipherText);

                if (cipherData.Length < Program.iVSize)
                    throw new InvalidOperationException("Invalid cipher text format");

                byte[] iv = new byte[Program.iVSize];
                byte[] encryptedData = new byte[cipherData.Length - Program.iVSize];

                Buffer.BlockCopy(cipherData, 0, iv, 0, Program.iVSize);
                Buffer.BlockCopy(cipherData, Program.iVSize, encryptedData, 0, encryptedData.Length);

                using var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
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
