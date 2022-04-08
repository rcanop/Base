using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Base.Encryption
{
    public class CryptoAES
    {
        private Byte[] _key;
        private Byte[] _iv;
        private Byte[] _encript;
        private string _decript;

        public string Encriptado { get => Convert.ToBase64String(_encript); }
        public string Desencriptado { get => _decript; }

        public CryptoAES(string key, string iv)
        {
            _key = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref _key, 32);
            _iv = Encoding.UTF8.GetBytes(iv);
            Array.Resize(ref _iv, 16);
        }
        public CryptoAES()
        {
            _key = new byte[] { 0x00 };
            Array.Resize(ref _key, 32);
            _iv = new byte[] { 0x00 };
            Array.Resize(ref _iv, 16);
        }

        public void Encrypt(string texto)
        {
            EncryptProcess(texto);
        }

        private void EncryptProcess(string text)
        {
            Encoding encoding = Encoding.Default;

            try
            {
                using (Aes aesEncrypt = Aes.Create())
                {
                    if (text == null || text.Length <= 0)
                        throw new ArgumentNullException("text");
                    if (_key == null || _key.Length <= 0)
                        throw new ArgumentNullException("Key");
                    if (_iv == null || _iv.Length <= 0)
                        throw new ArgumentNullException("IV");

                    aesEncrypt.Clear();
                    aesEncrypt.Mode = CipherMode.CBC;
                    aesEncrypt.Padding = PaddingMode.PKCS7;
                    aesEncrypt.KeySize = 256;
                    aesEncrypt.Key = _key;
                    aesEncrypt.IV = _iv;

                    ICryptoTransform encryptor = aesEncrypt.CreateEncryptor(aesEncrypt.Key, aesEncrypt.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, encoding))
                            {
                                swEncrypt.Write(text);
                            }
                            _encript = msEncrypt.ToArray();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e; ;
            }

        }
        public void Decrypt(string cipherText)
        {
            DecryptProcess(cipherText);
        }
        private void DecryptProcess(string textoCifrado)
        {
            if (textoCifrado == null || textoCifrado.Length <= 0)
                throw new ArgumentNullException("textoCifrado");

            Encoding encoding = Encoding.Default;
            byte[] encoded = Convert.FromBase64String(textoCifrado);

            using (Aes aesDecrypt = Aes.Create())
            {
                if (_key == null || _key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (_iv == null || _iv.Length <= 0)
                    throw new ArgumentNullException("IV");

                aesDecrypt.Clear();
                aesDecrypt.Mode = CipherMode.CBC;
                aesDecrypt.Padding = PaddingMode.PKCS7;
                aesDecrypt.KeySize = 256;
                aesDecrypt.Key = _key;
                aesDecrypt.IV = _iv;

                ICryptoTransform decryptor = aesDecrypt.CreateDecryptor(aesDecrypt.Key, aesDecrypt.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encoded))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt, encoding))
                        {
                            _decript = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

        }
        public static string EncryptAES(string key, string iv, string text)
        {
            CryptoAES cryptoAES = new CryptoAES(key, iv);
            cryptoAES.Encrypt(text);
            return cryptoAES.Encriptado;
        }
        public static string DecryptAES(string key, string iv, string textEncrypt)
        {
            CryptoAES cryptoAES = new CryptoAES(key, iv);
            cryptoAES.Decrypt(textEncrypt);
            return cryptoAES.Desencriptado;
        }
    }
}
