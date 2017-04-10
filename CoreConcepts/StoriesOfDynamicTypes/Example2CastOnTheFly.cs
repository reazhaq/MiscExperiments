// let's say, I have no control of some old/3rd party library
// and they have some base and derived classes but not much to help with polymorphism

using System;

namespace StoriesOfDynamicTypes
{
    class SomeBaseClass
    {
        public string SomeData { get; set; } = "Some_Base_Data";
    }

    class SomeDerivedClass : SomeBaseClass
    {
        public new string SomeData { get; set; } = "Some*Derived*Data";
    }

    class Example2CastOnTheFly
    {
        internal static void SomeMethod()
        {
            Console.WriteLine("-----> starting Example2CastOnTheFly.SomeMethod <-----");

            SomeBaseClass blah = new SomeDerivedClass();

            // this one goes to the method that takes the base class
            TellMeWhoYouAre(blah);

            // this one goes to the over loaded method
            TellMeWhoYouAre((dynamic)blah);

            // without dynamic - I have to see what kind it is
            if (blah is SomeBaseClass)
            {
                if (blah is SomeDerivedClass)
                    TellMeWhoYouAre((SomeDerivedClass)blah);
                else
                    TellMeWhoYouAre(blah); // base class always goes last
            }

            Console.WriteLine("-----> done Example2CastOnTheFly.SomeMethod <-----");
        }

        private static void TellMeWhoYouAre(SomeBaseClass blah)
        {
            Console.WriteLine($"blah.SomeData: {blah.SomeData}");
        }

        private static void TellMeWhoYouAre(SomeDerivedClass blahDerived)
        {
            Console.WriteLine($"blahDerived.SomeData: {blahDerived.SomeData}");
        }
    }
}
