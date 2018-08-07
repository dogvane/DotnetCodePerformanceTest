using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCode.Collection
{
    public class IntListAndDictionary10 : BaseIntListAndDictionary
    {
        public IntListAndDictionary10() : base(10)
        {

        }
    }

    public class IntListAndDictionary5 : BaseIntListAndDictionary
    {
        public IntListAndDictionary5() : base(5)
        {

        }
    }

    public class IntListAndDictionary20 : BaseIntListAndDictionary
    {
        public IntListAndDictionary20() : base(20)
        {

        }
    }

    public class IntListAndDictionary50 : BaseIntListAndDictionary
    {
        public IntListAndDictionary50() : base(50)
        {

        }
    }


    public class BaseIntListAndDictionary
    {
        int capacity = 5;
        int testCount = 1000;
        int idRang = 1000;

        List<int> testList = new List<int>();

        List<int> list = new List<int>();

        Dictionary<int, string> map = new Dictionary<int, string>();

        public BaseIntListAndDictionary(int capacity = 10)
        {
            this.capacity = capacity;

            Console.WriteLine("capacity:" + capacity);
            Random rand = new Random();

            for (var i = 0; i < capacity; i++)
            {
                var data = rand.Next(0, idRang);
                if (map.ContainsKey(data))
                {
                    i--;
                    continue;
                }

                list.Add(data);
                map[data] = data.ToString();
            }

            for (var i = 0; i < idRang; i++)
            {
                testList.Add(list[rand.Next(0, idRang) % capacity]);
            }
        }

        [Benchmark]
        public void TestListFor()
        {
            for (var i = 0; i < testCount; i++)
            {
                var findItem = testList[i];
                //var item = list.FirstOrDefault(o=> o == findItem);
                int item;
                for (var j = 0; j < list.Count; j++)
                {
                    if (list[j] == findItem)
                    {
                        item = list[j];
                        break;
                    }
                }
            }
        }

        [Benchmark]
        public void TestListForeach()
        {
            for (var i = 0; i < testCount; i++)
            {
                var findItem = testList[i];
                var item = list.FirstOrDefault(o => o == findItem);
            }
        }

        [Benchmark]
        public void TestDictionary()
        {
            for (var i = 0; i < testCount; i++)
            {
                var findItem = testList[i];
                string item;
                map.TryGetValue(findItem, out item);
            }
        }
    }
}
