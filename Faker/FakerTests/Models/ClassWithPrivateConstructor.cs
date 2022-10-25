using System;

namespace FakerTests.Models
{
    internal class ClassWithPrivateConstructor
    {
        private int a;
        public DateTime b;
        public C c;

        private ClassWithPrivateConstructor(int a, DateTime b)
        {
            this.a = a;
            this.b = b;
        }
    }
}