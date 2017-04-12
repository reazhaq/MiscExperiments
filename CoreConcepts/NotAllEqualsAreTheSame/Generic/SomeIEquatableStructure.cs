using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAllEqualsAreTheSame.Generic
{
    struct SomeIEquatableStructure : IEquatable<SomeIEquatableStructure>
    {
        public int SomeInternalData { get; set; }

        public bool Equals(SomeIEquatableStructure other)
        {
            return SomeInternalData == other.SomeInternalData;
        }
    }
}
