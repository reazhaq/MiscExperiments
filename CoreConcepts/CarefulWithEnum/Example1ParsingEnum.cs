using System;

namespace CarefulWithEnum
{
    enum Color
    {
        // starting with undefined is a good idea; specially when working with legacy code
        Undefined = 0,
        Red,
        White,
        Blue
    };

    class Example1ParsingEnum
    {
        internal static void SomeMethod()
        {
            Console.WriteLine("-----> starting Example1ParsingEnum.SomeMethod <-----");

            for (var loopIndex = 0; loopIndex < 6; loopIndex++)
            {
                // enum can happily convert any number to an enum value; even if not defined
                WhatIsMyColorFromANumber(loopIndex);
                // converting a string to an enum does the same when a number is passed as string
                WhatIsMyColorFromAString(loopIndex.ToString());
            }
            Console.Write(Environment.NewLine);

            string[] someColorStrings = { "7", "2", "blue", "Yellow", null, "red,white", "white,blue", "blue,green" };
            Console.WriteLine($"checking someColorStrings: {string.Join("; ", someColorStrings)}");
            // what to expect - yellow and null shall fail to parse; but what about "red,white"
            foreach(var someColorString in someColorStrings)
            {
                WhatIsMyColorFromAString(someColorString);
            }
            Console.Write(Environment.NewLine);

            // how to check if something is an enum correctly
            for (var loopIndex2=0; loopIndex2 < someColorStrings.Length; loopIndex2++)
            {
                if (IsThisAValidEnumValueInt(loopIndex2))
                    WhatIsMyColorFromANumber(loopIndex2);
                else
                    Console.WriteLine($"{loopIndex2} is not a valid enum number.");

                if (IsThisAValidEnumValueString(loopIndex2.ToString()))
                    WhatIsMyColorFromAString(loopIndex2.ToString());
                else
                    Console.WriteLine($"\"{loopIndex2}\" is not a valid enum string");

                var someColorString = someColorStrings[loopIndex2];
                Console.WriteLine($"Checking {someColorString} from someColorStrings array");
                if (IsThisAValidEnumValueString(someColorString))
                    WhatIsMyColorFromAString(someColorString);
                else
                    Console.WriteLine($"---->someColorString: \"{someColorString}\" is not a valid enum ");
            }

            Console.WriteLine("-----> done Example1ParsingEnum.SomeMethod <-----");
        }

        private static void WhatIsMyColorFromANumber(int someNumber)
        {
            var someColor = (Color)someNumber;
            Console.WriteLine($"someNumber: {someNumber} converts to {someColor}");
        }

        private static void WhatIsMyColorFromAString(string someText)
        {
            Console.WriteLine(Enum.TryParse(someText, true, out Color parsedColor)
                ? $"someText: \"{someText}\" converts to {parsedColor}"
                : $"someText: \"{someText}\" failed to parse");
        }

        private static bool IsThisAValidEnumValueInt(int someNumber)
        {
            // this always works with any number with no error
            var someColor = (Color)someNumber;
            // this one checks if it really exists in the def.
            return Enum.IsDefined(typeof(Color), someColor);
        }

        private static bool IsThisAValidEnumValueString(string someText)
        {
            // parse and validate if it really exists; that's because any number string parses with no error
            return Enum.TryParse(someText, true, out Color someColor)
                && Enum.IsDefined(typeof(Color), someColor);
        }
    }
}
