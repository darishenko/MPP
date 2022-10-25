using System;
using System.Collections;
using System.Collections.Generic;
using FieldCreators;

namespace ListCreatorLib
{
    internal class ListCreator : IGenericCreator
    {
        public Type CurType { get; }
        private Dictionary<Type, IPrimitiveTypeCreator> primitiveTypeCreator;

        public ListCreator(Dictionary<Type, IPrimitiveTypeCreator> primitiveTypeCreator)
        {
            CurType = typeof(List<>);
            this.primitiveTypeCreator = primitiveTypeCreator;
        }

        public object Create(Type type)
        {
            IList result = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            var length = new Random().Next(1, 10);

            if (primitiveTypeCreator.TryGetValue(type, out IPrimitiveTypeCreator creator))
            {
                for (int i = 0; i < length; i++) result.Add(creator.Create());
            }
            else
            {
                var defaultValue = "DEFAULT";
                for (int i = 0; i < length; i++) result.Add(defaultValue);
            }

            return result;
        }
    }
}