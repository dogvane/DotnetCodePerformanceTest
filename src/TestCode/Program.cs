using BenchmarkDotNet.Running;
using System;
using TestCode.Collection;

namespace TestCode
{
    class Program
    {
        static void Main(string[] args)
        {
            TestString();

            TestInt();
        }

        private static void TestString()
        {
            var summary = BenchmarkRunner.Run<ListAndDictionary5>();
            summary = BenchmarkRunner.Run<ListAndDictionary10>();
            summary = BenchmarkRunner.Run<ListAndDictionary20>();
            summary = BenchmarkRunner.Run<ListAndDictionary50>();
        }

        private static void TestInt()
        {
            var summary = BenchmarkRunner.Run<IntListAndDictionary5>();
            summary = BenchmarkRunner.Run<IntListAndDictionary10>();
            summary = BenchmarkRunner.Run<IntListAndDictionary20>();
            summary = BenchmarkRunner.Run<IntListAndDictionary50>();
        }
    }
}
