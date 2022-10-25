using System;

namespace FieldCreators.PrimitiveTypesGenerator
{
    public class LongGenerator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public LongGenerator()
        {
            CurType = typeof(long);
        }

        public object Create()
        {
            var number1 = new Random().Next();
            var number2 = new Random().Next();
            return (long) (number1 * number2);
        }
    }
}