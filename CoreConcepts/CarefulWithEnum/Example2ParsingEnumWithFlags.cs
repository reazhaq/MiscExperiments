using System;

namespace CarefulWithEnum
{
    [Flags]
    enum Color2
    {
        Undefined = 0,
        Red = 1,
        White = 2,
        Blue = 4,
        Green = 8,
        Pink = 16
    };

    class Example2ParsingEnumWithFlags
    {
        internal static void SomeMethod()
        {
            Console.WriteLine("-----> starting Example2ParsingEnumWithFlags.SomeMethod <-----");

            string[] colorStrings = { "0", "2", "8", "blue", "Blue", "Yellow", "Red, Green", "Red, white", "", null, "bar, foo", "Black", "green" };
            foreach (var colorString in colorStrings)
            {
                if (Enum.TryParse(colorString, true, out Color2 colorValue))
                {
                    var enumIsDefined = Enum.IsDefined(typeof(Color2), colorValue);
                    var containsComma = colorValue.ToString().Contains(",");

                    if (enumIsDefined | containsComma)
                        Console.WriteLine($"--->Converted '{colorString}' to {colorValue}.");
                    else
                        Console.WriteLine($"--->{colorString} is not an underlying value of the Colors enumeration.");
                }
                else
                    Console.WriteLine($"--->{colorString} is not a member of the Colors enumeration.");
            }

            Console.WriteLine("-----> done Example2ParsingEnumWithFlags.SomeMethod <-----");
        }
    }
}
