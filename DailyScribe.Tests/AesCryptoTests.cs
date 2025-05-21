using DailyScribe.Cryptography.AES;
using System.Security.Cryptography;

namespace DailyScribe.Tests
{
    public class AesCryptoTests
    {
        readonly static byte[] key = RandomNumberGenerator.GetBytes(32);

        readonly string plainText = "test.txt";

        [Fact]
        public void Encrypt_Should_Return_Encrypted_Data()
        {
            var encryptor = new AES_encryption();

            //

            var encrypted = encryptor.Encrypt(plainText, key);

            Assert.False(string.IsNullOrEmpty(encrypted));
            Assert.NotEqual(encrypted, plainText);
        }

        [Fact]
        public void Decrypt_Should_Return_Decrypted_Data()
        {
            var encryptor = new AES_encryption();

            //

            var encrypted = encryptor.Encrypt(plainText, key);

            var decryptor = new AES_decryption();

            //

            var decrypted = decryptor.Decrypt(encrypted, key);

            //

            Assert.Equal(plainText, decrypted);
        }

        [Fact]
        public void Master_Key_Should_Return_Not_Valid()
        {
            var validKey = key;

            var notValidKey = RandomNumberGenerator.GetBytes(32);

            //

            var encrypted = new AES_encryption().Encrypt(plainText, key);

            var decrypted = new AES_decryption();

            // padding is invalid
            Assert.Throws<InvalidOperationException>(() =>
            {
                decrypted.Decrypt(encrypted, notValidKey);
            });
        }
    }
} 

