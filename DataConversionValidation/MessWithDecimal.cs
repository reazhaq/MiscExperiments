using System;

namespace DataConversionValidation
{
    public class MessWithDecimal
    {
        public static bool CheckDecimalToXDecimalPlaces(decimal decimalValue, int digits) =>
            decimal.Round(decimalValue, digits) == decimalValue;
    }
}
