using System;

namespace BaseAndDerive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var t = new Triangle {Name = "Triangle", Id = 99};
            var t = new Triangle {Name = "Triangle"};
            var b = (Shape) t;
        }
    }
}
