using System;

namespace NotAllEqualsAreTheSame.Generic
{
    class SomeIEquatableClass : IEquatable<SomeIEquatableClass>
    {
        public int SomeValue { get; set; }

        // Equals are always used for comparing internals of two objects; not ref. equal
        // implementation of IEquatable
        // this one happily takes base and derived class
        public virtual bool Equals(SomeIEquatableClass other)
        {
            if (other is null) return false;

            return SomeValue == other.SomeValue;
        }

        // with equals override - need to check for null
        // and object of same type
        public override bool Equals(object obj)
        {
            //if (obj is null || obj.GetType() != GetType() ) return false;
            if (obj is null || !(obj is SomeIEquatableClass)) return false;

            return SomeValue == ((SomeIEquatableClass)obj).SomeValue;
        }

        public override int GetHashCode()
        {
            return SomeValue.GetHashCode();
        }
    }
}
