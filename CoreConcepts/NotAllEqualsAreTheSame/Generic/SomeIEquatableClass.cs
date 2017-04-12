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
    }
}
