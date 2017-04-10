using System;

namespace StoriesOfDynamicTypes
{
    class Example1ReturnAndUseAnonymousObject
    {

        // if returning object don't make you feel good - skip GetAnAnonymousObject1 and use GetAnAnonymousObject2
        public static void SomeMethod()
        {
            Console.WriteLine("-----> starting Example1ReturnAndUseAnonymousObject.SomeMethod <-----");

            // here and object is assigned to dynamic
            dynamic someDynamicObject = GetAnAnonymousObject1();
            Console.WriteLine($"someDynamicObject.FirstName = {someDynamicObject.FirstName}");

            // here a dynamic is returned
            dynamic someOtherDynamicObject = GetAnAnonymousObject2();
            Console.WriteLine($"someOtherDynamicObject.FirstName = {someOtherDynamicObject.FirstName}");

            Console.WriteLine("-----> done Example1ReturnAndUseAnonymousObject.SomeMethod <-----");
        }

        private static object GetAnAnonymousObject1()
        {
            return new { FirstName = "fn", LastName = "ln", Age = 22 };
        }

        private static dynamic GetAnAnonymousObject2()
        {
            return new { FirstName = "fn2", LastName = "ln2", Age = 24 };
        }
    }
}
