using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Exception = System.Exception;


/// <summary>
/// Task.WhenAll returns a task -> call it combined task
///   when await on combined task - it throws the first exception
///   when .Result or WaitAll instead of when-all - it throws an aggregate exception
/// </summary>

namespace AggregateExceptionExperiment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //await Experiment01.Method1(); //// this one throws an unhandled exception
            //await Experiment02.Method2();

            try
            {
                //await Experiment03.AggregateExceptionExperiment();
                Experiment03.AggregateExceptionExperiment().Wait();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"exception caught in main:{Environment.NewLine}{e}");
            }
        }
    }
}
