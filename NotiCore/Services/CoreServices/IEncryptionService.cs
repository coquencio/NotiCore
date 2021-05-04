using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices
{
    public interface IEncryptionService
    {
        string Encrypt(string encryptString);
        string Decrypt(string cipherText);
        string GetKey();
        string BuildKeys(List<(string key, string value)> keys);
        List<(string key, string value)> GetKeys(string encrypted);
    }
}
