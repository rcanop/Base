using Base.Validations;
using Xunit;

namespace Base.Test
{
    public class CifTests
    {
        [Theory]
        [InlineData("H64746399", true)]
        [InlineData("", false)]
        [InlineData("N7744582C", true)]
        [InlineData("Q1427495E", true)]
        [InlineData("P244A564I", false)]
        public void IsFormatted(string codigo, bool isOK)
        {
            bool  result = Cif.IsFormatted(codigo);
            Assert.Equal(isOK, result);
        }

        [Theory]
        [InlineData("H64746399", true)]
        [InlineData("A06671457", true)]
        [InlineData("N7744582C", true)]
        [InlineData("Q1427495E", true)]
        [InlineData("P2440564I", true)]
        public void IsOK(string codigo, bool isOK)
        {
            bool result = Cif.IsOK(codigo);
            Assert.Equal(isOK, result);
        }

    }
}
