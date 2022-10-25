using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    internal class BoolCreator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public BoolCreator()
        {
            CurType = typeof(bool);
        }

        public object Create()
        {
            var number = new Random().Next();
            return number % 2 == 0;
        }
    }
}