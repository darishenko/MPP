using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    public class ShortGenerator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public ShortGenerator()
        {
            CurType = typeof(short);
        }

        public object Create()
        {
            return (short) new Random().Next();
        }
    }
}