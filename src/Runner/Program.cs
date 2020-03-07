using BenchmarkDotNet.Running;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<MappingBenchmarks>();
        }
    }
}
