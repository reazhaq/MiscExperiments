using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAllEqualsAreTheSame
{
    struct PointStructureEquatable : IEquatable<PointStructureEquatable>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PointStructureEquatable(int x, int y)
        {
            X = x; Y = y;
        }

        public bool Equals(PointStructureEquatable other)
        {
            return X == other.X && Y == other.Y;
        }

        public static bool operator==(PointStructureEquatable p1, PointStructureEquatable p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator!=(PointStructureEquatable p1, PointStructureEquatable p2)
        {
            return !(p1.Equals(p2));
        }

        // once you override the static operator; it is going to complain about not implementing hash code and equals
        // this override doesn't help; it still forces you to do boxing
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PointStructureEquatable))
                return false;

            return this == ((PointStructureEquatable)obj);
        }

        // caution - hash code override need to be done correctly
        public override int GetHashCode()
        {
            return (X.GetHashCode() * 17 + Y.GetHashCode());
        }
    }
}
