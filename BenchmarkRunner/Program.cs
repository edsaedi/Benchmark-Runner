using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BenchmarkRunner
{
    [AttributeUsage(AttributeTargets.Method)]
    class BenchmarkAttribute : Attribute
    {

    }

    class BenchmarkRunner
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
                        method.Invoke(null, null);
                    }
                }
                //Finish the function. Print out method and the mean of the functions.
            }
        }
    }

    class BenchmarkTests
    {
        const int size = 100000;

        [Benchmark]
        public void StringConcat()
        {
            string str = "";

            for (int i = 0; i < size; i++)
            {
                str += "A";
            }
        }

        [Benchmark]
        public void StringBuilderAppend()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                stringBuilder.Append("A");
            }

        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}
