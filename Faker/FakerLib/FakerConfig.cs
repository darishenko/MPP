using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FieldCreators;


namespace FakerLib
{
    public class FakerConfig : IFakerConfig
    {
        public Dictionary<PropertyInfo, IPrimitiveTypeCreator> creators;

        public void Add<TClass, TProperty, TPrimitive>(Expression<Func<TClass, TProperty>> expression)
            where TClass : class
            where TPrimitive : IPrimitiveTypeCreator
        {
            var expressionBody = expression.Body;
            var creator = (IPrimitiveTypeCreator) Activator.CreateInstance(typeof(TPrimitive));
            if (creator.CurType != typeof(TProperty))
                throw new ArgumentException("Types of creators aren't match");
            creators.Add((PropertyInfo) ((MemberExpression) expressionBody).Member, creator);
        }

        public FakerConfig()
        {
            creators = new Dictionary<PropertyInfo, IPrimitiveTypeCreator>();
        }
    }
}