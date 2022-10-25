using System;

namespace FieldCreators
{
    public interface IGenericCreator : IType
    {
        object Create(Type type);
    }
}