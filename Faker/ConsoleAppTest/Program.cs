namespace FakerLib;

internal class A
{
    public B bClass;
}

internal class B
{
    public char character;
    public A aClass;
    public C cClass;
}

internal class C
{
    public List<uint> list;
    public A aClass;
}

internal class Program
{
    public static void Main()
    {
        var _faker = new Faker();
        _faker.Create<int>();
        Console.WriteLine(_faker.Create<int>());
        var aa = _faker.Create<A>();
        Console.WriteLine(aa);
    }
}