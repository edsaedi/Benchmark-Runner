using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace BenchmarkRunner
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BenchmarkAttribute : Attribute
    {

    }

    public class BenchmarkRunner
    {
        public static void Run<T>()
        {
            Type tType = typeof(T);


            var methods = tType.GetMethods();
            //Method and Mean of functions to print out.


            foreach (var method in methods)
            {
                var methodAttributes = method.GetCustomAttributes();

                foreach (var methodAttribute in methodAttributes)
                {
                    if (methodAttribute is BenchmarkAttribute)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            method.Invoke(null, null);
                        }
                        
                        double totalElapsedTime = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            var stopwatch = Stopwatch.StartNew();
                            method.Invoke(null, null);
                            stopwatch.Stop();
                            totalElapsedTime += stopwatch.Elapsed.TotalMilliseconds;
                            Console.WriteLine($"{method.Name}: Elapsed={stopwatch.Elapsed.TotalMilliseconds}");
                        }

                        Console.WriteLine($"{method.Name}: Average Time={totalElapsedTime / 3}");
                    }
                }

                //Finish the function. Print out method and the mean of the functions.

            }
        }
    }

    //class BenchmarkTests
    //{
    //    const int size = 100000;

    //    [Benchmark]
    //    public static void StringConcat()
    //    {
    //        string str = "";

    //        for (int i = 0; i < size; i++)
    //        {
    //            str += "A";
    //        }
    //    }

    //    [Benchmark]
    //    public static void StringBuilderAppend()
    //    {
    //        StringBuilder stringBuilder = new StringBuilder();

    //        for (int i = 0; i < size; i++)
    //        {
    //            stringBuilder.Append("A");
    //        }

    //    }
    //}


    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        BenchmarkRunner.Run<BenchmarkTests>();
    //    }
    //}
}
