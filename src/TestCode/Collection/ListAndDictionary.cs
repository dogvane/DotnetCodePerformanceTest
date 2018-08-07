using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCode.Collection
{
    public class ListAndDictionary10 : BaseListAndDictionary
    {
        public ListAndDictionary10():base(10)
        {

        }
    }

    public class ListAndDictionary5 : BaseListAndDictionary
    {
        public ListAndDictionary5() : base(5)
        {

        }
    }

    public class ListAndDictionary20 : BaseListAndDictionary
    {
        public ListAndDictionary20() : base(20)
        {

        }
    }

    public class ListAndDictionary50 : BaseListAndDictionary
    {
        public ListAndDictionary50() : base(50)
        {

        }
    }

    public class BaseListAndDictionary
    {
        int capacity = 0;
        int testCount = 1000;
        int idRang = 1000;

        List<string> testList = new List<string>();

        List<string> list = new List<string>();

        Dictionary<string, string> map = new Dictionary<string, string>();

        public BaseListAndDictionary(int capacity = 10)
        {
            this.capacity = capacity;

            Random rand = new Random();

            for(var i = 0;i < capacity; i++)
            {
                var data = rand.Next(0, idRang).ToString();
                if(map.ContainsKey(data))
                {
                    i--;
                    continue;
                }

                list.Add(data);
                map[data] = data;
            }

            for(var i =0;i < idRang; i++)
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
                string item = null;
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
        public void TestListFirstOrDefault()
        {
            for(var i =0;i < testCount; i++)
            {
                var findItem = testList[i];
                var item = list.FirstOrDefault(o=> o == findItem);
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
