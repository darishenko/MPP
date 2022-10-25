using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    public class ByteGenerator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public ByteGenerator()
        {
            CurType = typeof(byte);
        }

        public object Create()
        {
            return (byte) new Random().Next(1, byte.MaxValue);
        }
    }
}