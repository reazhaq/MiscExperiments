using System;

namespace NotAllEqualsAreTheSame
{
    class Example1Simple
    {
        internal static void SomeMethod()
        {
            // when possible use operator == and !=
            OperatorUse();
        }

        private static void OperatorUse()
        {
            // when objects supports == and != use it
            int x = 5; int y = 5;
            var areEqual = x == y;

            // careful it doesn't work for everything
            var areObjsEqual = ((object)x) == ((object)y);

            // unless, the type has specific overrides
            var areStrsEqual = (x.ToString()) == (y.ToString());
        }
    }
}
