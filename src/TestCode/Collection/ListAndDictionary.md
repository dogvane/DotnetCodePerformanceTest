# List 与 Dictonary 在查询效率上的分界线

在读性能分析的文章的时候，刚好见过一个提示，在小数据量的情况下，使用Dictonary会比较占用内存空间，效率上也不必List的好多少，所以建议小数据量的时候，可以使用List。

文章只是介绍细节，没有实证，因此随手写了一个demo来验证一下这个过程。

初始化的核心是List和Dictionary的长度和数据一致，作为测试用例的数据也要同一份，测试用的数据随机生成。
比较的重点在不同数据量的情况下，两种类型的查询效率之比。

```
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
```

List的查询用了2种方法，一种是在For循环里比对，一种使用了Linq。


```

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
```
测试环境如下：

···
BenchmarkDotNet=v0.11.0, OS=Windows 10.0.10240.17202 (1507/RTM/Threshold1)
Intel Xeon CPU E5-1607 0 3.00GHz, 1 CPU, 4 logical and 4 physical cores
Frequency=2923049 Hz, Resolution=342.1085 ns, Timer=TSC
.NET Core SDK=2.1.300
  [Host]     : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
···

测试的结果数据如下：

int|5|10|20|50
---|---|---|---|---|
TestListFor|0.0570 us|0.0447 us|0.3559 us|0.0171 us
TestListFirstOrDefault|0.2354 us|2.5835 us|5.6384 us|0.7374 us
TestDictionary|0.5711 us|0.1078 us|0.5004 us|0.0074 us

string|5|10|20|50
---|---|---|---|---|
TestListFor|0.0115 us|0.0539 us|0.0565 us|2.0825 us
TestListFirstOrDefault|0.4236 us|0.4179 us|1.9821 us|12.9455 us
TestDictionary|0.7195 us|0.0120 us|1.4515 us|0.0343 us

结果数据的波动幅度比较大，测试了几次也是这样，可能和测试环境与测试用例的数据有关。但总体的比例关系是确定的。结论如下：

1. List用for循环查询快，Linq的FirstOrDefault是比较慢的，4~5倍的差距。
2. Dictionary在20次左右查询效率会逐渐超过List。
3. 只要不是热点函数，以及大数据（量级请自测），爱用那个用那个。
