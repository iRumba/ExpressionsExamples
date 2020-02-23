using BenchmarkDotNet.Running;
using System;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CloneBenchmarks>();
            Console.WriteLine("Hello World!");
        }
    }
}
