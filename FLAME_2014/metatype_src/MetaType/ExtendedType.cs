using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MetaType
{
    public class ExtendedType
    {
        public ExtendedType(Type type, ExtendedType baseType)
        {
            Type = type;
            BaseType = baseType;
        }

        private readonly IDictionary<string, Func<object, object[], object>> _methods = new Dictionary<string, Func<object, object[], object>>();

        public Type Type { get; private set; }
        public Type ProxyType { get; set; }
        public ExtendedType BaseType { get; private set; }

        public void SetMethod(string name, Delegate target)
        { Bind(name, target); }

        public void SetMethod<T>(string name, Func<T> target)
        { Bind(name, target); }
        public void SetMethod<T1, T>(string name, Func<T1, T> target)
        { Bind(name, target); }
        public void SetMethod<T1, T2, T>(string name, Func<T1, T2, T> target)
        { Bind(name, target); }
        public void SetMethod<T1, T2, T3, T>(string name, Func<T1, T2, T3, T> target)
        { Bind(name, target); }
        public void SetMethod<T1, T2, T3, T4, T>(string name, Func<T1, T2, T3, T4, T> target)
        { Bind(name, target); }
        public void SetMethod(string name, Action target)
        { Bind(name, target); }
        public void SetMethod<T1>(string name, Action<T1> target)
        { Bind(name, target); }
        public void SetMethod<T1, T2>(string name, Action<T1, T2> target)
        { Bind(name, target); }
        public void SetMethod<T1, T2, T3>(string name, Action<T1, T2, T3> target)
        { Bind(name, target); }


        public void SetMissingMethod(Func<MissingMethodCall, object> missingMethod)
        { Bind("MissingMethod", missingMethod); }

        public void SetMissingMethod<T>(Func<T, MissingMethodCall, object> missingMethod)
        { Bind("MissingMethod", missingMethod); }

        public Func<object, object[], object> FindMethod(string name)
        {
            for (var scan = this; scan != null; scan = scan.BaseType)
            {
                Func<object, object[], object> method;
                if (scan._methods.TryGetValue(name, out method))
                    return method;
            }
            return null;
        }

        public Func<object, object[], object> FindMethodOnType(string name)
        {
            Func<object, object[], object> method;
            if (_methods.TryGetValue(name, out method))
                return method;
            return null;
        }


        private void Bind(string name, Delegate target)
        {
            _methods[name] = (proxy, args) => CallMethod(target, proxy, args);
        }

        static object CallMethod(Delegate target, object proxy, object[] arguments)
        {
            var parameters = target.Method.GetParameters();
            var invokeArguments = arguments ?? new object[0];
            if (parameters.Length == invokeArguments.Length + 1 &&
                parameters[0].ParameterType.IsAssignableFrom(proxy.GetType()))
            {
                if (arguments == null || arguments.Length == 0)
                {
                    invokeArguments = new[] { proxy };
                }
                else
                {
                    invokeArguments = new[] { proxy }.Concat(arguments).ToArray();
                }
            }

            return target.DynamicInvoke(invokeArguments);
        }

    }
}
