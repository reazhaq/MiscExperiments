using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAllEqualsAreTheSame
{
    class Program
    {
        static void Main(string[] args)
        {
            AreTwoPointsEqual();
        }

        private static void AreTwoPointsEqual()
        {
            // are two points equal
            var p1Struct = new PointStructure(x: 5, y: 5);
            var p2Struct = new PointStructure(x: 5, y: 5);

            //// this can't be used; static operator == doesn't exists for any structure; it does for class
            //// these has to be cast to objects; and than use the operator for the class
            //Console.WriteLine($"p1Struct == p2Struct: {(p1Struct == p2Struct ? "true" : "false")}");

            var p1Object = (object)p1Struct;
            var p2Object = (object)p2Struct;
            var p1ObjectEqualsP2Object = p1Object == p2Object;

            // false
            Console.WriteLine($"p1Object == p1Object: {p1ObjectEqualsP2Object}");

            var p1StructEqualsP2Struct = p1Struct.Equals(p2Struct);
            // false - but boxing involved
            Console.WriteLine($"p1Struct.Equals(p2Struct): {p1StructEqualsP2Struct}");
        }

        private static void AreTwoPointsWithStaticEquals_Equal()
        {
            var p1 = new PointStructureWithStaticEqual(5, 5);
            var p2 = new PointStructureWithStaticEqual(5, 5);

            // now - we don't have to cast it to objects
            // lot better - no boxing
            var p1EqualsP2 = p1 == p2;
            Console.WriteLine($"p1 == p2: {p1EqualsP2}");

            // but Equals; still forces boxes
            var p1EqP2 = p1.Equals(p2);
            Console.WriteLine($"p1.Equals(p2): {p1EqP2}");
        }
    }
}
