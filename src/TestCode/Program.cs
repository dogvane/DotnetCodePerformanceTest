using BenchmarkDotNet.Running;
using System;
using TestCode.Collection;

namespace TestCode
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ListAndDictionary50>();

            BenchmarkRunner.Run<IntListAndDictionary20>();
            return;

            TestString();

            TestInt();
        }

        private static void TestString()
        {
            BenchmarkRunner.Run<ListAndDictionary5>();
            BenchmarkRunner.Run<ListAndDictionary10>();
            BenchmarkRunner.Run<ListAndDictionary20>();
            BenchmarkRunner.Run<ListAndDictionary50>();
        }

        private static void TestInt()
        {
            BenchmarkRunner.Run<IntListAndDictionary5>();
            BenchmarkRunner.Run<IntListAndDictionary10>();
            BenchmarkRunner.Run<IntListAndDictionary20>();
            BenchmarkRunner.Run<IntListAndDictionary50>();
        }
    }
}
