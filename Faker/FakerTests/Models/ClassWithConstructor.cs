using System;

namespace FakerTests.Models
{
    internal class ClassWithConstructor
    {
        private int a;
        public DateTime b;
        public C c;

        private ClassWithConstructor(int a, DateTime b)
        {
            this.a = a;
            this.b = b;
        }
    }
}