using System;

namespace BaseAndDerive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var t1 = new Triangle {Name = "Triangle", Id = 99};
            var t2 = new Triangle {Name = "Triangle"};
            var b1 = (Shape) t1;
            Console.WriteLine($"t1.Id: {t1.Id}; b1.Id: {b1.Id}");

            var b2 = (Shape) t2;
            Console.WriteLine($"b2.Id: {b2.Id}, t2.Id: {t2.Id}");

            t2.Id = 55;
            Console.WriteLine($"b2.Id: {b2.Id}, t2.Id: {t2.Id}");
            b2.Id = 44;
            Console.WriteLine($"b2.Id: {b2.Id}, t2.Id: {t2.Id}");
        }
    }
}
