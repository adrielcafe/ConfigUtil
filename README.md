ConfigUtil
==========
This utility class enables you to load and save your configuration class into a XML file.

Thanks to *System.Xml.Serialization* it's easy to serialize and deserializa objects.

You can save *string*, *int*, *long*, *float*, *decimal*, *List*, *DateTime* and so on!

How to use
----------
Let's suppose you have a POCO like this:
```csharp
public class MyConfig
{
    public string myString;
    public int myInt;
    public decimal myDecimal;
    public List<string> myList;
}

static class Program
{
    MyConfig cfg = new MyConfig();
    cfg.myString = "my string";
    cfg.myInt = 123;
    cfg.myDecimal = 10.25m;
    cfg.myList = new List<string>() { "a", "b", "c" };
}
```

The default config filename is *config.xml*, but you can change it:
```csharp
ConfigUtil.fileName = "myConfig.xml"; // Default value is config.xml
```

Save the object this way:
```csharp
ConfigUtil.Save(cfg);
```

And for load it:
```csharp
MyConfig cfg = ConfigUtil.Load<MyConfig>();
Console.WriteLine(cfg.myString);
Console.WriteLine(cfg.myInt);
Console.WriteLine(cfg.myDecimal);
Console.WriteLine(string.Format("{0}, {1}, {2}", cfg.myList[0], cfg.myList[1], cfg.myList[2]));
```

The XML configuration file will be like this:
```xml
<?xml version="1.0" encoding="utf-8"?>
<MyConfig xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <myString>my string</myString>
  <myInt>123</myInt>
  <myDecimal>10.25</myDecimal>
  <myList>
    <string>a</string>
    <string>b</string>
    <string>c</string>
  </myList>
</MyConfig>
```


#### Simple like that!