using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    internal class CharCreator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public CharCreator()
        {
            CurType = typeof(char);
        }

        public object Create()
        {
            return (char) new Random().Next(1, 64);
        }
    }
}