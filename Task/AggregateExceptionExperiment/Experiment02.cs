using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AggregateExceptionExperiment
{
    public static class Experiment02
    {
        public static async Task Method2()
        {
            var sw = Stopwatch.StartNew();
            var t1 = Task21();
            var t2 = Task22();
            var t3 = Task23();

            try
            {
                var combinedTask = Task.WhenAll(t1, t2, t3);
                var results = await combinedTask;  // this one throws one exception
                //var results = combinedTask.Result;   // this one blocks and throws aggregate exception

                foreach (var result in results)
                {
                    Debug.WriteLine($"   result: {result}");
                }

                //Task.WaitAll(t1, t2, t3);  ///// this one blocks and throws aggregate exception; note void return
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
                await t1.ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.Faulted)
                        Debug.WriteLine($"####### t1 exception: {t.Exception?.Flatten()}");
                }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                await t2.ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.Faulted)
                        Debug.WriteLine($"####### t2 exception: {t.Exception?.Flatten()}");
                }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                await t3.ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.Faulted)
                    {
                        Debug.WriteLine($"####### t3 exception: {t.Exception?.Flatten()}");
                    }
                }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

                sw.Stop();
                Debug.WriteLine($"### original exception caught was:{Environment.NewLine}{exception}");
            }

            Debug.WriteLine($"--- sw: {sw.ElapsedMilliseconds} ms");
            //////////////////////////////////////
            Task<int> Task23()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new Exception("foo"));
                return tcs.Task;
            }

            Task<int> Task22()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new TimeoutException("blah blah"));
                return tcs.Task;
            }

            Task<int> Task21()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetResult(5);
                return tcs.Task;
            }
        }
    }
}