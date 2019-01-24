using System;
using DataConversionValidation;
using Xunit;
using Xunit.Abstractions;

namespace DataConversionValidationTests
{
    public class MessWithDecimalTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MessWithDecimalTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(0.00, 2, true)]
        [InlineData(3.333, 2, false)]
        [InlineData(3.333, 3, true)]
        [InlineData(5.5, 2, true)]
        [InlineData(5.5, 1, true)]
        [InlineData(5.5, 0, false)]
        [InlineData(5.2, 0, false)]
        [InlineData(7, 2, true)]
        [InlineData(0.0002, 2, false)]
        public void CheckForTwoDecimalPlaces(decimal d, int decimalPlacesToCheck, bool expectedResult)
        {
            _testOutputHelper.WriteLine($"{d}");
            _testOutputHelper.WriteLine($"{decimal.Round(d, decimalPlacesToCheck)}");
            var result = MessWithDecimal.CheckDecimalToXDecimalPlaces(d, decimalPlacesToCheck);
            _testOutputHelper.WriteLine($"{d} is {result}");

            Assert.Equal(expectedResult, result);
        }
    }
}
