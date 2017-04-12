namespace NotAllEqualsAreTheSame.Generic
{
    // this class overrides Equals; to ensure value equality
    // and forced to override hash code; because of Equals override
    class SomeClass
    {
        public int SomeData { get; set; }

        // with equals override - need to check for null
        // and object of same type (passing a derived class shall not work)
        // if biz rule says - want to compare a base class object and derived
        // class object - then, changet the obj.GetType... to !(obj is SomeClass)
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType() ) return false;

            return SomeData == ((SomeClass)obj).SomeData;
        }

        public override int GetHashCode()
        {
            return SomeData.GetHashCode();
        }
    }
}
