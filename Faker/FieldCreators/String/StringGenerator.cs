using System;

namespace FieldCreators.String
{
    internal class StringCreator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public StringCreator()
        {
            CurType = typeof(string);
        }

        public object Create()
        {
            var random = new Random();
            var bytesArray = new byte[random.Next(1, 100)];
            random.NextBytes(bytesArray);
            return bytesArray.ToString();
        }
    }
}