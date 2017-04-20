using NotAllEqualsAreTheSame.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NotAllEqualsAreTheSame
{
    class Example3EqInsideGenerics
    {
        internal static void SomeMethod()
        {
            var someGenericClassThatDoesSomeComparisons = new SomeGenericClassThatDoesSomeComparisons<SomeClass>();
            someGenericClassThatDoesSomeComparisons.SetValue(null);
            someGenericClassThatDoesSomeComparisons.SetValue(new SomeClass { SomeData = 5 });
            someGenericClassThatDoesSomeComparisons.SetValue2(null);
            try
            {
                someGenericClassThatDoesSomeComparisons.SetValue2(new SomeClass {SomeData = 10});
            }
            catch
            {
                Debug.WriteLine("this is a known exception");
                someGenericClassThatDoesSomeComparisons.SetValue(new SomeClass { SomeData = 10 });
            }
            someGenericClassThatDoesSomeComparisons.SetValue2(new SomeDerivedClass {SomeData = 10});

            var someGenericClassThatDoesSomeComparisonsBetterCls = new SomeGenericClassThatDoesSomeComparisonsBetter<SomeIEquatableClass>();
            someGenericClassThatDoesSomeComparisonsBetterCls.SetValue(null);
            someGenericClassThatDoesSomeComparisonsBetterCls.SetValue(new SomeIEquatableClass { SomeValue = 99 });

            var someGenericClassThatDoesSomeComparisonsBetterStr = new SomeGenericClassThatDoesSomeComparisonsBetter<SomeIEquatableStructure>();
            someGenericClassThatDoesSomeComparisonsBetterStr.SetValue(new SomeIEquatableStructure { SomeInternalData = 88 });
            someGenericClassThatDoesSomeComparisonsBetterStr.SetValue(new SomeIEquatableStructure { SomeInternalData = 88 });
        }
    }

    class SomeGenericClassThatDoesSomeComparisons<T>
    {
        public T SomeProperty { get; private set; }

        public void SetValue(T newValue)
        {
            if (object.Equals(SomeProperty, newValue))
                return;

            SomeProperty = newValue;
        }

        public void SetValue2(T newValue)
        {
            // calling virtual equal has its issue
            // what if SomeProperty is null
            if (SomeProperty.Equals(newValue))
                return;

            SomeProperty = newValue;
        }
    }

    class SomeGenericClassThatDoesSomeComparisonsBetter<T> where T : IEquatable<T>
    {
        public T SomeProperty { get; private set; }

        public void SetValue(T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(SomeProperty, newValue))
                return;

            SomeProperty = newValue;
        }
    }
}
