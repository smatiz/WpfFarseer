using System;
using System.Collections.Generic;
using System.Reflection;

namespace MetaType
{
    public class ExtendedObject
    {
        public ExtendedObject(ExtendedType baseType)
        {
            _baseType = baseType;
        }

        private readonly IDictionary<string, object> _state = new Dictionary<string, object>();

        private readonly ExtendedType _baseType;
        private ExtendedType _instanceType;


        public ExtendedType GetExtendedType()
        {
            return _instanceType ?? _baseType;
        }

        private ExtendedType GetInstanceType()
        {
            if (_instanceType == null)
            {
                lock (this)
                {
                    if (_instanceType == null)
                        _instanceType = new ExtendedType(null, _baseType);
                }
            }
            return _instanceType;
        }

        public bool TryGet(object proxy, string name, out object value)
        {
            if (_state.TryGetValue(name, out value))
                return true;

            if (TryInvokeMember(proxy, "get_" + name, null, out value))
                return true;

            value = null;
            return false;
        }


        public object Set(string name, object value)
        {
            if (value is Delegate)
            {
                GetInstanceType().SetMethod(name, (Delegate)value);
            }
            else
            {
                _state[name] = value;
            }
            return value;
        }

        public bool TryInvokeMember(object proxy, string name, object[] arguments, out object value)
        {
            var method = GetExtendedType().FindMethod(name);
            if (method != null)
            {
                value = method(proxy, arguments);
                return true;
            }

            if (name != "MissingMethod")
            {
                var callArray = new object[1];
                var call = new MissingMethodCall(FindMissingMethods(), m => m(proxy, callArray))
                           {
                               Name = name,
                               Arguments = arguments,
                           };
                callArray[0] = call;
                value = call.Default();
                if (value != MissingMethodCall.Absent)
                    return true;
            }

            value = null;
            return false;
        }

        private IEnumerable<Func<object, object[], object>> FindMissingMethods()
        {
            for (var scan = GetExtendedType(); scan != null; scan = scan.BaseType)
            {
                var method = scan.FindMethodOnType("MissingMethod");
                if (method != null)
                    yield return method;

                if (scan.Type != null)
                {
                    
                    var clrMethod = scan.Type.GetMethod("MissingMethod", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    
                    if (clrMethod != null)
                        yield return (proxy, args) => clrMethod.Invoke(proxy, args);
                }
            }
        }
    }
}
