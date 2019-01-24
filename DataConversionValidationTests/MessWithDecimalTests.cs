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
        [InlineData(0.00, true)]
        [InlineData(3.333, false)]
        [InlineData(5.5, true)]
        [InlineData(7, true)]
        [InlineData(0.0002, false)]
        public void CheckForTwoDecimalPlaces(decimal d, bool expectedResult)
        {
            _testOutputHelper.WriteLine($"{d}");
            _testOutputHelper.WriteLine($"{decimal.Round(d, 2)}");
            var result = MessWithDecimal.CheckDecimalToXDecimalPlaces(d, 2);
            _testOutputHelper.WriteLine($"{d} is {result}");

            Assert.Equal(expectedResult, result);
        }
    }
}
