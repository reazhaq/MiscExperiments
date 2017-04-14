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
        }

        private static void OperatorUse()
        {
            // when objects supports == and != use it
            int x = 5; int y = 5;
            var areEqual = x == y;

            // careful it doesn't work for everything - this gives you false
            var areObjsEqual = ((object)x) == ((object)y);

            // unless, the type has specific overrides
            // but string (ref type) have special overload
            var areStrsEqual = (x.ToString()) == (y.ToString());

            // some types makes sure that == works in a different way
            double someDouble = double.NaN;
            var areNanEquals = someDouble == someDouble;
        }

        private static void VirtualEqualsHasToWork()
        {
            double someDouble = double.NaN;
            // this is false
            var areNanEquals = someDouble == someDouble;
            // this is true
            var vEqlWorks = someDouble.Equals(someDouble);
            // it is also reflexive
            var vEqlReflexive = double.NaN.Equals(someDouble);
            var vEqlReflexive2 = someDouble.Equals(double.NaN);
        }
    }
}
