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

            string[] colorStrings = { "0", "2", "8", "blue", "Blue", "Yellow", "Red, Green", "Red, white", "", null, "bar, foo", "Black", "green", "22", "99" };
            Console.WriteLine($"checking values in colorStrings: {string.Join("; ", colorStrings)}");
            foreach (var colorString in colorStrings)
            {
                if (Enum.TryParse(colorString, true, out Color2 colorValue))
                {
                    var enumIsDefined = Enum.IsDefined(typeof(Color2), colorValue);
                    var containsComma = colorValue.ToString().Contains(",");
                    var isFlagDefined = IsFlagDefined(colorValue);
                    Console.WriteLine($"{colorString} is parsed. " +
                                      $"IsDefined: {enumIsDefined}; " +
                                      $"containsComma: {containsComma}; " +
                                      $"isFlagDefined: {isFlagDefined}");

                    if (enumIsDefined | containsComma)
                        Console.WriteLine($"--->Converted '{colorString}' to {colorValue}.");
                    else
                        Console.WriteLine($"--->{colorString} is not an underlying value of the Colors enumeration.");
                }
                else
                    Console.WriteLine($"--->{colorString} is not a member of the Colors enumeration.");
            }

            // another fun little thing with flags...
            // when you have 6 flags starting with zero...
            // you actually have 32 enums
            for (int i = 0; i <= 35; i++)
            {
                var someColor = (Color2)i;
                Console.WriteLine($"{IsFlagDefined(someColor)}: {i} = {someColor}");
            }

            Console.WriteLine("-----> done Example2ParsingEnumWithFlags.SomeMethod <-----");
        }

        // there is no easy way to check if an enum is a flag or not
        // I can look for comma; like I had done above
        static bool IsFlagDefined(Enum e)
        {
            Console.WriteLine($"***IsFlagDefined for {e} using int parsing: {!int.TryParse(e.ToString(), out var foo1)}");
            Console.WriteLine($"***IsFlagDefined for {e} using decimal parsing: {!decimal.TryParse(e.ToString(), out var foo2)}");
            return !decimal.TryParse(e.ToString(), out var someDecimalValue);
        }
    }
}
