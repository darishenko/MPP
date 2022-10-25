using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    public class DoubleGenerator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public DoubleGenerator()
        {
            CurType = typeof(double);
        }

        public object Create()
        {
            return new Random().NextDouble();
        }
    }
}