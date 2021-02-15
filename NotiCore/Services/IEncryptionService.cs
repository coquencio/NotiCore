using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string encryptString);
        string Decrypt(string cipherText);
        string GetKey();
    }
}
