using System;
using System.Diagnostics;

namespace CollectionExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var foo = new ClassWithReadonlyCollection();
            Debug.WriteLine($"foo.ReadOnlyCollection is {(foo.ReadOnlyCollection == null ? "null" : "not-null")}"); //null
        }
    }
}
