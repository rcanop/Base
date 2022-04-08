using Base.Encryption;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Base.Test
{
    public class CryptoAESTests
    {
        [Fact]
        public void Encrypt_String_And_Decrypt_Returns_Same_String()
        {
            string encryptedText = CryptoAES.EncryptAES("", "", "Contraseña");
            string decryptedText = CryptoAES.DecryptAES("", "", encryptedText);

            Assert.Equal("Contraseña", decryptedText);
        }
        [Theory]
        [InlineData("", "", "Contraseña", "YK5a7r1ETq2h/GS6JOmegA==")]
        public void Encrypt_String_Expected_Result(string key, string iv, string text, string encripted)
        {
            string encryptedText = CryptoAES.EncryptAES(key, iv, text);

            Assert.Equal(encripted, encryptedText);
        }

        [Theory]
        [InlineData("", "", "YK5a7r1ETq2h/GS6JOmegA==", "Contraseña")]
        public void Decrypt_String_Expected_Result(string key, string iv, string encripted, string text)
        {
            string decryptedText = CryptoAES.DecryptAES(key, iv, encripted);

            Assert.Equal(text, decryptedText);
        }
    }
}
