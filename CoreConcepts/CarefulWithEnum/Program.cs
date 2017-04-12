using System;

namespace CarefulWithEnum
{
    class Program
    {
        static void Main(string[] args)
        {
            var someColor = (Color)2;
            MostCommonMistake(someColor);
            someColor = (Color)99;
            MostCommonMistake(someColor); // this ends up printing blue

            Example1ParsingEnum.SomeMethod();
            Example2ParsingEnumWithFlags.SomeMethod();
        }

        private static void MostCommonMistake(Color someColor)
        {
            if (someColor == Color.Undefined)
                Console.WriteLine("got Undefined color");
            else if (someColor == Color.Red)
                Console.WriteLine("got red");
            else if (someColor == Color.White)
                Console.WriteLine("got white");
            else
                Console.WriteLine("got blue"); // this is incorrect assumption
        }
    }
}
