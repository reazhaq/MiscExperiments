using System;

namespace NotAllEqualsAreTheSame
{
    class Example1Simple
    {
        internal static void SomeMethod()
        {
            // when possible use operator == and !=
            OperatorUse();
            // virtual Equals always guaranteed to work
            VirtualEqualsHasToWork();
            // strings are immutable - it is a lie
            StringsAreSillyToo();
        }

        private static void OperatorUse()
        {
            // when objects supports == and != use it
            int x = 5; int y = 5;
            var areEqual = x == y;
            Console.WriteLine($"x==y: {areEqual}");

            // careful it doesn't work for everything - this gives you false
            var areObjsEqual = ((object)x) == ((object)y);
            Console.WriteLine($"((object)x) == ((object)y): {areObjsEqual}");

            // unless, the type has specific overrides
            // but string (ref type) have special overload
            var areStrsEqual = (x.ToString()) == (y.ToString());
            Console.WriteLine($"(x.ToString()) == (y.ToString()): {areStrsEqual}");
        }

        private static void VirtualEqualsHasToWork()
        {
            double someDouble = double.NaN;
            // this is false
            var areNanEquals = someDouble == someDouble;
            Console.WriteLine($"a double may not always be == to itself: {areNanEquals}");

            // this is true
            var vEqlWorks = someDouble.Equals(someDouble);
            Console.WriteLine($"but - .Equals method has to work for all doubles: someDouble.Equals(someDouble)={vEqlWorks}");

            // it is also reflexive
            var vEqlReflexive = double.NaN.Equals(someDouble);
            var vEqlReflexive2 = someDouble.Equals(double.NaN);
            Console.WriteLine($".Equals is always reflexive: double.NaN.Equals(someDouble)={vEqlReflexive}, someDouble.Equals(double.NaN)={vEqlReflexive2}");
        }

        private static void StringsAreSillyToo()
        {
            string s1 = "test";
            string s2 = "test";
            string s3 = "test1".Substring(0, 4);
            object s4 = s3;
            string s5 = new string("test".ToCharArray());

            // test, test, test, test
            Console.WriteLine($"s1:{s1}, s2:{s2}, s3:{s3}, s4:{s4}, s5:{s5}");
            
            // true, true, true -- how come two different strings have the same ref.
            Console.WriteLine($"object.ReferenceEquals(s1, s2):{object.ReferenceEquals(s1, s2)}, " + //true
                              $"s1 == s2:{s1 == s2}, " + //true
                              $"s1.Equals(s2):{s1.Equals(s2)}"); //true

            Console.WriteLine($"object.ReferenceEquals(s1, s5):{object.ReferenceEquals(s1, s5)}, " + //false
                              $"object.ReferenceEquals(s3, s4):{object.ReferenceEquals(s3, s4)}, " + // true
                              $"object.ReferenceEquals(s3, s5):{object.ReferenceEquals(s3, s5)}"); // false

            // false, true, true
            Console.WriteLine($"object.ReferenceEquals(s1, s3):{object.ReferenceEquals(s1, s3)}, " + //false
                              $"s1 == s3:{s1 == s3}, " + // true
                              $"s1.Equals(s3):{s1.Equals(s3)}"); //true

            // false, false, true -- s1==s4; because of one being object - it goes for ref. equal; not string == operator overload
            Console.WriteLine($"object.ReferenceEquals(s1, s4):{object.ReferenceEquals(s1, s4)}, " + //false
                              $"s1 == s4:{s1 == s4}, " + //false
                              $"s1.Equals(s4):{s1.Equals(s4)}, " + //true
                              $"s4.Equals(s1):{s4.Equals(s1)}"); //true
        }
    }
}


