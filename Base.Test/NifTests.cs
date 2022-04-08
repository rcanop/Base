using Base.Validations;
using Xunit;

namespace Base.Test
{
    public class NifTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("55937123L", true)]
        [InlineData("Z1498420K", true)]
        [InlineData("X4243249W", true)]
        [InlineData("Y5652034M", true)]
        [InlineData("41282364", false)]
        public void Code_IsFormatted(string codigo, bool isOK)
        {
            bool result = Nif.IsFormatted(codigo);
            Assert.Equal(isOK, result);
        }

        [Theory]
        [InlineData("55937123L", true)]
        [InlineData("Z1498420K", true)]
        [InlineData("X4243249W", true)]
        [InlineData("Y5652034M", true)]
        [InlineData("41282364D", true)]
        public void Code_IsOK(string codigo, bool isOK)
        {
            bool result = Nif.IsOK(codigo);
            Assert.Equal(isOK, result);
        }
    }
}