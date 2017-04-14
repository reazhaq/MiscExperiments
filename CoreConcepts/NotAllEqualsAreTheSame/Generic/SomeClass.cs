namespace NotAllEqualsAreTheSame.Generic
{
    // this class overrides Equals; to ensure value equality
    // and forced to override hash code; because of Equals override
    class SomeClass
    {
        public int SomeData { get; set; }

        // with equals override - need to check for null
        // and object of same type
        public override bool Equals(object obj)
        {
            //if (obj is null || obj.GetType() != GetType() ) return false;
            if (obj is null || !(obj is SomeClass)) return false;

            return SomeData == ((SomeClass)obj).SomeData;
        }

        public override int GetHashCode()
        {
            return SomeData.GetHashCode();
        }
    }
}
