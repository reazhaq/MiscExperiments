﻿using System;
using System.Threading;

namespace PrematureGcCleanup471
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer(MyTimerCallback, null, 2000, 500);
            Console.WriteLine("timer started - hit enter to stop");
            //GC.Collect(); // if this line enabled... only debug works
            //GC.KeepAlive(timer); // to fix 
            Console.ReadLine();
        }

        private static void MyTimerCallback(object state)
        {
            Console.WriteLine($"Datetime.Now: {DateTime.Now}");
        }
    }
}
