using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    public class IntGenerator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public IntGenerator()
        {
            CurType = typeof(int);
        }

        public object Create()
        {
            return new Random().Next();
        }
    }
}