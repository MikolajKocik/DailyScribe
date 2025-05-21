using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe.Cryptography.AES.Interfaces
{
    public interface IAES_decryption
    {
        public string Decrypt(string cipherText, byte[] masterKey);

    }
}
