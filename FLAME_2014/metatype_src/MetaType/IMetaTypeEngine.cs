using System;

namespace MetaType
{
    public interface IMetaTypeEngine
    {
        ExtendedType GetType<T>();
        ExtendedType GetType(Type type);

        T New<T>(params object[] constructorParameters);
        object New(Type type, params object[] constructorParameters);
    }
}