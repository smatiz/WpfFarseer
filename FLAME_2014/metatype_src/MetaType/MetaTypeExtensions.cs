using System;

namespace MetaType
{
    public static class MetaTypeExtensions
    {
        public static void SetMissingMethod<T>(
            this IMetaTypeEngine engine,
            Func<MissingMethodCall, object> missingMethod)
        {
            engine.GetType<T>().SetMissingMethod(missingMethod);
        }

        public static void SetMissingMethod<T>(
            this IMetaTypeEngine engine, 
            Func<T, MissingMethodCall, object> missingMethod)
        {
            engine.GetType<T>().SetMissingMethod(missingMethod);
        }

    }
}