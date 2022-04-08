using Base.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Base.Test
{
    public class ReferenciaCatastralTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("8496104NG1789N0001ZW", true)]
        [InlineData("0141803DR3704S0053RR", true)]
        [InlineData("0W41803DR3704S0053RR", false)]
        [InlineData("0141803DR3704S0053R0", false)]
        [InlineData("0141803DR3704S0053R", false)]
        public void IsFormatted(string codigo, bool isOK)
        {
            bool result = ReferenciaCatastral.IsFormatted(codigo);
            Assert.Equal(isOK, result);
        }
        [Theory]
        [InlineData("4991201WF3649S0047UQ", true)]
        [InlineData("8496104NG1789N0001ZW", true)]
        [InlineData("0141803DR3704S0053RR", true)]
        [InlineData("000300700CS64G0001PH", true)]
        [InlineData("30033A004001680000FI", true)]
        [InlineData("7426513VK3672N0006XA", true)]
        [InlineData("15001A005006120000JL", true)]
        public void IsOK(string codigo, bool isOK)
        {
            bool result = ReferenciaCatastral.IsOK(codigo);
            Assert.Equal(isOK, result);
        }
    }
}
