using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AggregateExceptionExperiment
{
    public static class Experiment03
    {
        public static async Task AggregateExceptionExperiment()
        {
            var sw = Stopwatch.StartNew();
            var t1 = Task21();
            var t2 = Task22();
            var t3 = Task23();
            var t4 = Task24();

            try
            {
                var combinedTask = Task.WhenAll(t1, t2, t3, t4);
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
                await t4.ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.Faulted && t.Exception != null)
                    {
                        var rethrow = false;
                        foreach (var ex in t.Exception.InnerExceptions)
                        {
                            if (ex is CustomException customException)
                            {
                                Debug.WriteLine($"-----> t4 exception: {customException}");
                            }
                            else
                            {
                                //Debug.WriteLine($"***** exception: {ex}");
                                //throw ex; // throw it out..
                                rethrow = true;
                            }
                        }

                        if (rethrow) throw t.Exception.Flatten();
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

            Task<int> Task24()
            {
                var tcs = new TaskCompletionSource<int>();
                var e1 = new Exception("moo");
                var e2 = new CustomException("moo");
                var e3 = new TimeoutException("coo");
                var e4 = new DivideByZeroException("loo");
                var ae = new AggregateException(e1,e2,e3,e4);
                tcs.SetException(ae);
                return tcs.Task;
            }
        }

        class CustomException : Exception
        {
            public CustomException(string msg) : base(msg){}
        }
    }
}