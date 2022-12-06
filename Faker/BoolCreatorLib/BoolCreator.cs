using System;
using FieldCreators;

namespace BoolCreatorLib
{
    internal class BoolCreator : IPrimitiveTypeCreator
    {
        private IPrimitiveTypeCreator _primitiveTypeCreatorImplementation;
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