using System.Collections.Generic;

namespace FakerTests.Models
{
    internal class A
    {
        public int integer;
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
        public List<int> list;
        public A aClass;
    }
}