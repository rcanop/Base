using Base.Validations;
using System;
using Xunit;

namespace Base.Test
{
    public class IdufirTests
    {
        const string SPACE14 = "              ";
        const string NULLSTRING = null;
        const string EMPTYSTRING = "";
        const string IDUFIROK = "11111111111113";
        const string IDUFIRKO = "11111111231113";

        [Theory]
        [InlineData(NULLSTRING)]
        [InlineData(EMPTYSTRING)]
        [InlineData(SPACE14)]
        public void EmptyOrNullString_IsOK_False(string cad)
        {
            bool isOK = Idufir.IsOK(cad);

            Assert.False(isOK);
        }
        [Theory]
        [InlineData(NULLSTRING)]
        [InlineData(EMPTYSTRING)]
        [InlineData(SPACE14)]
        public void EmptyOrNullString_IsFormatted_False(string cad)
        {
            bool isOK = Idufir.IsFormatted(cad);

            Assert.False(isOK);
        }
        [Fact]
        public void IdufirOK_IsOK_True()
        {
            bool isOK = Idufir.IsOK(IDUFIROK);

            Assert.True(isOK);
        }
        [Fact]
        public void IdufirKO_IsOK_False()
        {
            bool isOK = Idufir.IsOK(IDUFIRKO);

            Assert.False(isOK);
        }
        [Fact]
        public void IdufirOK_IsFormatted_True()
        {
            bool isOK = Idufir.IsFormatted(IDUFIROK);

            Assert.True(isOK);
        }
        [Theory]
        [InlineData(NULLSTRING)]
        [InlineData(EMPTYSTRING)]
        [InlineData(SPACE14)]
        [InlineData("01234567890123A")]
        [InlineData("01234567890123.")]
        public void IdufirOK_IsFormatted_False(string cad)
        {
            bool isOK = Idufir.IsFormatted(cad);

            Assert.False(isOK);
        }

        [Theory]
        [InlineData("1111111111111", "3")]
        [InlineData("11111111111113","3")]
        public void Idufir_CalculateDC_OK(string cad, string dcOK)
        {
            string dc = Idufir.CalculateDC(cad);

            Assert.Equal(dcOK, dc);
        }

        [Theory]
        [InlineData("1111111111112", "3")]
        [InlineData("1111111111100", "3")]
        public void Idufir_CalculateDC_KO(string cad, string dcOK)
        {
            string dc = Idufir.CalculateDC(cad);

            Assert.NotEqual(dcOK, dc);
        }
    }
}
