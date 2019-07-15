using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AggregateExceptionExperiment
{
    public static class Experiment01
    {
        //// this one throws an unhandled exception
        //// because exception is thrown from the method; not from a task
        //// to correctly throw an exception from a task - use task-completion-source (see Method2)
        public static async Task Method1()
        {
            var t1 = Task1();
            var t2 = Task2();
            var t3 = Task3();

            var sw = Stopwatch.StartNew();
            try
            {
                var combinedTask = Task.WhenAll(t1, t2, t3);
                await combinedTask;
                //combinedTask.GetAwaiter().GetResult();

                Debug.WriteLine($"combinedTask completed in {combinedTask.Status}");

                ////combinedTask.Wait();

                //////Task.WaitAll(t1, t2, t3);
                //await t1;
                //await t2;
                //await t3;

                sw.Stop();
            }
            catch (AggregateException aggregateException)
            {
                sw.Stop();
                aggregateException.Handle(ex =>
                {
                    Debug.WriteLine(
                        $"*** Exception caught in aggregate:{Environment.NewLine}{ex}");
                    return true;
                });
            }
            catch (Exception exception)
            {
                sw.Stop();
                Debug.WriteLine($"exception:{Environment.NewLine}{exception}");
            }

            Debug.WriteLine($"sw: {sw.ElapsedMilliseconds} ms");
            //return Task.CompletedTask;


            Task Task2()
            {
                Task.Delay(100);
                throw new TimeoutException("blah");
            }

            Task Task1() => Task.Delay(2000);

            Task Task3()
            {
                Task.Delay(200);
                throw new ArgumentNullException();
            }
        }
    }
}