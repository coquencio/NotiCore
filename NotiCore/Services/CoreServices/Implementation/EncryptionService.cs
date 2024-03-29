﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aes = System.Security.Cryptography.Aes;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class EncryptionService : IEncryptionService
    {
        private string _encryptionKey;
        public EncryptionService(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }

        public string Encrypt(string encryptString)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[] 
                    {
                        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    }
                );
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public string GetKey()
        {
            return _encryptionKey;
        }

        public string BuildKeys(List<(string key, string value)> keys)
        {
            var encrypted = new StringBuilder();
            foreach (var key in keys)
            {
                encrypted.Append(key.key + " "); 
                encrypted.Append(key.value + ",");
            }
            var toEncrypt = encrypted.ToString();
            toEncrypt = toEncrypt.Substring(0, toEncrypt.Length - 1);
            return Encrypt(toEncrypt);
        }
        public List<(string key, string value)> GetKeys(string encrypted)
        {
            var list = new List<(string key, string value)>();
            var decrypted = Decrypt(encrypted);
            var keys = decrypted.Split(",");
            foreach (var key in keys)
            {
                var substracted = key.Split(" ");
                if (substracted.Length > 2)
                {
                    list.Add((substracted[0], substracted[1] +" "+ substracted[2]));
                }
                else
                {
                    list.Add((substracted[0], substracted[1]));
                }
            }
            return list;
        }
    }
}
