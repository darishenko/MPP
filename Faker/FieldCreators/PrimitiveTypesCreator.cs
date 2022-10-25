using System;
using System.Collections.Generic;
using FieldCreators.String;
using FieldCreators.Date;
using FieldCreators.PrimitiveTypesGenerator;

namespace FieldCreators
{
    public static class PrimitiveTypesCreator
    {
        public static Dictionary<Type, IPrimitiveTypeCreator> GetPrimitiveTypes()
        {
            var dict = new Dictionary<Type, IPrimitiveTypeCreator>();

            AddToDict(dict, new BoolCreator());
            AddToDict(dict, new ByteGenerator());
            AddToDict(dict, new DoubleGenerator());
            AddToDict(dict, new FloatGenerator());
            AddToDict(dict, new IntGenerator());
            AddToDict(dict, new LongGenerator());
            AddToDict(dict, new ShortGenerator());
            AddToDict(dict, new DateTimeCreator());
            AddToDict(dict, new StringCreator());
            AddToDict(dict, new CharCreator());

            return dict;
        }

        private static void AddToDict(Dictionary<Type, IPrimitiveTypeCreator> dict, IPrimitiveTypeCreator creator)
        {
            dict.Add(creator.CurType, creator);
        }
    }
}