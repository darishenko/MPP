using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    public class FloatGenerator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public FloatGenerator()
        {
            CurType = typeof(float);
        }

        public object Create()
        {
            return (float) new Random().NextDouble();
        }
    }
}