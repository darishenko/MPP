using System;
using System.Linq.Expressions;
using FieldCreators;

namespace FakerLib
{
    public interface IFakerConfig
    {
        void Add<TClass, TProperty, TPrimitive>(Expression<Func<TClass, TProperty>> expression)
            where TClass : class
            where TPrimitive : IPrimitiveTypeCreator;
    }
}