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

            var combinedTask = Task.WhenAll(t1, t2, t3, t4);

            try
            {
                var results = await combinedTask;  // this one throws one exception
                //var results = combinedTask.Result;   // this one blocks and throws aggregate exception

                foreach (var result in results)
                {
                    Debug.WriteLine($"   result: {result}");
                }

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
            catch
            {
                sw.Stop();

                var rethrow = false;
                foreach (var ex in combinedTask.Exception.InnerExceptions)
                {
                    if (ex is CustomException customException)
                        Debug.WriteLine($"------------ custom: {customException}");
                    else
                    {
                        Debug.WriteLine($"------------ custom: {ex}");
                        rethrow = true;
                    }
                }

                if (rethrow)
                {
                    Debug.WriteLine($"### elapsed {sw.ElapsedMilliseconds} in ms");
                    throw combinedTask.Exception.Flatten();
                }
                //await t1.ContinueWith(t =>
                //{
                //    if (t.Status == TaskStatus.Faulted)
                //        Debug.WriteLine($"####### t1 exception: {t.Exception?.Flatten()}");
                //}, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                //await t2.ContinueWith(t =>
                //{
                //    if (t.Status == TaskStatus.Faulted)
                //        Debug.WriteLine($"####### t2 exception: {t.Exception?.Flatten()}");
                //}, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                //await t3.ContinueWith(t =>
                //{
                //    if (t.Status == TaskStatus.Faulted)
                //    {
                //        Debug.WriteLine($"####### t3 exception: {t.Exception?.Flatten()}");
                //    }
                //}, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                //await t4.ContinueWith(t =>
                //{
                //    if (t.Status == TaskStatus.Faulted && t.Exception != null)
                //    {
                //        var rethrow = false;
                //        foreach (var ex in t.Exception.InnerExceptions)
                //        {
                //            if (ex is CustomException customException)
                //            {
                //                Debug.WriteLine($"-----> t4 exception: {customException}");
                //            }
                //            else
                //            {
                //                //Debug.WriteLine($"***** exception: {ex}");
                //                //throw ex; // throw it out..
                //                rethrow = true;
                //            }
                //        }

                //        if (rethrow) throw t.Exception.Flatten();
                //    }
                //}, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine($"### elapsed {sw.ElapsedMilliseconds} in ms");
            }

            Debug.WriteLine($"--- sw: {sw.ElapsedMilliseconds} ms");
            //////////////////////////////////////
            Task<int> Task23()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new Exception("task with Exception"));
                return tcs.Task;
            }

            Task<int> Task22()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new TimeoutException("task with timeout"));
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
                //var ae = new AggregateException(e1, e2, e3, e4);
                tcs.SetException(new []{ e1,e2,e3,e4});
                //tcs.SetResult(99);
                return tcs.Task;
            }
        }

        public static async Task AggregateExceptionExperiment2()
        {
            var sw = Stopwatch.StartNew();
            var t1 = Task21();
            var t2 = Task22();
            var t3 = Task23();
            var t4 = Task24();

            var combinedTask = Task.WhenAll(t1, t2, t3, t4);

            try
            {
                var results = await combinedTask;  // this one throws one exception
                //var results = combinedTask.Result;   // this one blocks and throws aggregate exception

                foreach (var result in results)
                {
                    Debug.WriteLine($"   result: {result}");
                }

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
            catch(Exception exception)
            {
                sw.Stop();
                Debug.WriteLine($"### elapsed {sw.ElapsedMilliseconds} in ms");
                Debug.WriteLine($"first exception caught: {exception}");
                Debug.WriteLine($"**** t1.IsFaulted: {t1.IsFaulted}");
                _ = t1.ContinueWith(t => HandleException(t, "t1"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine($"**** t2.IsFaulted: {t2.IsFaulted}");
                _ = t2.ContinueWith(t => HandleException(t, "t2"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine($"**** t3.IsFaulted: {t3.IsFaulted}");
                _ = t3.ContinueWith(t => HandleException(t, "t3"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine($"**** t4.IsFaulted: {t4.IsFaulted}");
                _ = t4.ContinueWith(t => HandleException(t, "t4"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine("**** done");
            }

            Debug.WriteLine($"--- sw: {sw.ElapsedMilliseconds} ms");
            //////////////////////////////////////

            void HandleException(Task<int> task, string taskName)
            {
                Debug.WriteLine($"***{taskName} task.IsFaulted: {task.IsFaulted}");
                if (task.Exception?.InnerExceptions != null)
                {
                    foreach (var innerException in task.Exception.InnerExceptions)
                    {
                        Debug.WriteLine(
                            $"****** innerException caught: {innerException}");
                    }
                }
            }

            Task<int> Task23()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new Exception("task23 Exception"));
                return tcs.Task;
            }

            Task<int> Task22()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new TimeoutException("task22 Timeout Exception"));
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
                var e2 = new CustomException("foo");
                var e3 = new TimeoutException("coo");
                var e4 = new DivideByZeroException("loo");
                //var ae = new AggregateException(e1, e2, e3, e4);
                tcs.SetException(new[] { e1, e2, e3, e4 });
                //tcs.SetResult(99);
                return tcs.Task;
            }
        }

        public static async Task AggregateExceptionExperiment3()
        {
            await Task.Delay(0);
            var sw = Stopwatch.StartNew();

            //var combinedTask = Task.WhenAll(t1, t2, t3, t4);
            var combinedTask = Task.Factory.StartNew(() =>
            {
                var child1 = Task.Factory.StartNew(() =>
                {
                    var child2 = Task.Factory.StartNew(() =>
                    {
                        // This exception is nested inside three AggregateExceptions.
                        throw new CustomException("Attached child2 faulted - with CustomException");
                    }, TaskCreationOptions.AttachedToParent);

                    // This exception is nested inside two AggregateExceptions.
                    throw new Exception("Attached child1 faulted - with Exception");
                }, TaskCreationOptions.AttachedToParent);
            });


            try
            {
                combinedTask.Wait();
                sw.Stop();
            }
            catch (AggregateException aggregateException)
            {
                sw.Stop();
                foreach (var e in aggregateException.Flatten().InnerExceptions)
                {
                    if (e is CustomException)
                    {
                        Debug.WriteLine($"custom exception: {e.Message}");
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            Debug.WriteLine($"--- sw: {sw.ElapsedMilliseconds} ms");
            //////////////////////////////////////
        }


        /// <summary>
        ///  this is based off msdn example
        /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/exception-handling-task-parallel-library
        /// this seems to not work - idea of only handle custom exception; throw the rest out doesn't work
        /// </summary>
        /// <returns></returns>
        public static async Task AggregateExceptionExperiment4()
        {
            var sw = Stopwatch.StartNew();
            var t1 = Task21();
            var t2 = Task22();
            var t3 = Task23();
            var t4 = Task24();
            var t5 = Task.WhenAll(t3, t4);


            try
            {
                Task.WaitAll(t1, t2, t5);
                sw.Stop();
            }
            catch (AggregateException aggregateException)
            {
                sw.Stop();
                //aggregateException.Handle(ex =>
                //{
                //    Debug.WriteLine(
                //        $"*** Exception caught in aggregate:{Environment.NewLine}{ex}");
                //    return true;
                //});

                Debug.WriteLine("Aggregate Exception caught....");
                foreach (var e in aggregateException.Flatten().InnerExceptions)
                {
                    Debug.WriteLine($"----- exception is {e}");
                    if (e is CustomException)
                    {
                        Debug.WriteLine($"*********** CustomException caught: {e.Message}");
                    }
                    else
                    {
                        Debug.WriteLine("**************** throwing it back to the wild");
                        throw;
                    }
                }
            }
            catch (Exception exception)
            {
                sw.Stop();
                Debug.WriteLine($"### elapsed {sw.ElapsedMilliseconds} in ms");
                Debug.WriteLine($"first exception caught: {exception}");
                Debug.WriteLine($"**** t1.IsFaulted: {t1.IsFaulted}");
                _ = t1.ContinueWith(t => HandleException(t, "t1"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine($"**** t2.IsFaulted: {t2.IsFaulted}");
                _ = t2.ContinueWith(t => HandleException(t, "t2"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine($"**** t3.IsFaulted: {t3.IsFaulted}");
                _ = t3.ContinueWith(t => HandleException(t, "t3"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine($"**** t4.IsFaulted: {t4.IsFaulted}");
                _ = t4.ContinueWith(t => HandleException(t, "t4"),
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Debug.WriteLine("**** done");
            }

            Debug.WriteLine($"--- sw: {sw.ElapsedMilliseconds} ms");
            //////////////////////////////////////

            void HandleException(Task<int> task, string taskName)
            {
                Debug.WriteLine($"***{taskName} task.IsFaulted: {task.IsFaulted}");
                if (task.Exception?.InnerExceptions != null)
                {
                    foreach (var innerException in task.Exception.InnerExceptions)
                    {
                        Debug.WriteLine(
                            $"****** innerException caught: {innerException}");
                    }
                }
            }

            Task<int> Task23()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new Exception("task23 Exception"));
                return tcs.Task;
            }

            Task<int> Task22()
            {
                var tcs = new TaskCompletionSource<int>();
                tcs.SetException(new TimeoutException("task22 Timeout Exception"));
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
                var e2 = new CustomException("foo");
                var e3 = new TimeoutException("coo");
                var e4 = new DivideByZeroException("loo");
                //var ae = new AggregateException(e1, e2, e3, e4);
                tcs.SetException(new[] { e1, e2, e3, e4 });
                //tcs.SetResult(99);
                return tcs.Task;
            }
        }

        class CustomException : Exception
        {
            public CustomException(string msg) : base(msg){}
        }
    }
}