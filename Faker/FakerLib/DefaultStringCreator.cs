using System;
using FieldCreators;

namespace FakerLib
{
    public class DefaultStringCreator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public DefaultStringCreator()
        {
            CurType = typeof(string);
        }

        public object Create()
        {
            return "default";
        }
    }
}