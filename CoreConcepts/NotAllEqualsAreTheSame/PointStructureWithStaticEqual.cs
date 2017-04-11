using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAllEqualsAreTheSame
{
    struct PointStructureWithStaticEqual
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointStructureWithStaticEqual(int x, int y)
        {
            X = x;
            Y = y;
        }

        // at the minimum; override the static equal operator; this get statically compiled and it is fast
        public static bool operator==(PointStructureWithStaticEqual p1, PointStructureWithStaticEqual p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        // if you override == operator; have to overrider != in pair
        public static bool operator!=(PointStructureWithStaticEqual p1, PointStructureWithStaticEqual p2)
        {
            return !(p1 == p2);
        }

        // once you override the static operator; it is going to complain about not implementing hash code and equals
        // this override doesn't help; it still forces you to do boxing
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PointStructureWithStaticEqual))
                return false;

            return this==((PointStructureWithStaticEqual)obj);
        }

        // caution - hash code override need to be done correctly
        public override int GetHashCode()
        {
            return (X.GetHashCode() * 17 + Y.GetHashCode());
        }
    }
}
