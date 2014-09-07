using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Castle.DynamicProxy;
using Castle.DynamicProxy;

namespace MetaType
{
    public class MetaTypeEngine : IMetaTypeEngine
    {
        private readonly Dictionary<Type, ExtendedType> _extendedTypes = new Dictionary<Type, ExtendedType>();

        public MetaTypeEngine()
            : this(new DefaultProxyBuilder())
        {
        }

        public MetaTypeEngine(IProxyBuilder proxyBuilder)
        {
            ProxyBuilder = proxyBuilder;
        }

        public IProxyBuilder ProxyBuilder { get; set; }

        private static readonly Type[] AdditionalInterfaces = new[]
        {
            typeof(IDynamicMetaObjectProvider), 
            typeof(IExtendedObjectProvider)
        };

        public ExtendedType GetType<T>()
        {
            return GetType(typeof(T));
        }

        public ExtendedType GetType(Type type)
        {
            ExtendedType extendedType;
            if (!_extendedTypes.TryGetValue(type, out extendedType))
            {
                lock (_extendedTypes)
                {
                    if (!_extendedTypes.TryGetValue(type, out extendedType))
                    {
                        extendedType = BuildType(type);
                        _extendedTypes.Add(type, extendedType);
                    }
                }
            }
            return extendedType;
        }

        private ExtendedType BuildType(Type type)
        {
            ExtendedType baseType = null;
            if (type != typeof(Object))
                baseType = GetType(type.BaseType);

            return new ExtendedType(type, baseType);
        }

        public T New<T>(params object[] constructorParameters)
        {
            return (T)CreateInstance(typeof(T), constructorParameters);
        }

        public object New(Type type, params object[] constructorParameters)
        {
            return CreateInstance(type, constructorParameters);
        }

        private object CreateInstance(Type type, IEnumerable<object> constructorParameters)
        {
            var extendedType = GetType(type);
            if (extendedType.ProxyType == null)
            {
                extendedType.ProxyType = ProxyBuilder.CreateClassProxyType(
                    type,
                    AdditionalInterfaces,
                    ProxyGenerationOptions.Default);
            }

            var instance = new ExtendedObject(extendedType);

            return Activator.CreateInstance(
                extendedType.ProxyType,
                ConcatArgs(
                    new InstanceInterceptor(instance),
                    constructorParameters));
        }

        private static object[] ConcatArgs(IInterceptor interceptor, IEnumerable<object> constructorParameters)
        {
            return constructorParameters != null && constructorParameters.Any()
                       ? new object[] { new[] { interceptor } }.Concat(constructorParameters).ToArray()
                       : new object[] { new[] { interceptor } };
        }
    }
}
