using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using Castle.DynamicProxy;

namespace MetaType
{
    class InstanceInterceptor : IInterceptor
    {
        private readonly ExtendedObject _extendedObject;
        private static readonly MethodInfo DynamicMetaObjectProviderGetMetaObject = typeof(IDynamicMetaObjectProvider).GetMethod("GetMetaObject");
        private static readonly MethodInfo InstanceProviderGetInstanceObject = typeof(IExtendedObjectProvider).GetMethod("GetExtendedObject");

        public InstanceInterceptor(ExtendedObject extendedObject)
        {
            _extendedObject = extendedObject;
        }

        public void Intercept(IInvocation invocation)
        {
            object temp;
            if (invocation.Method == DynamicMetaObjectProviderGetMetaObject)
            {
                invocation.ReturnValue = new InstanceMetaObject(
                    (Expression)invocation.GetArgumentValue(0), 
                    invocation.Proxy);
            }
            else if (invocation.Method == InstanceProviderGetInstanceObject)
            {
                invocation.ReturnValue = _extendedObject;
            }
            else if (_extendedObject.TryInvokeMember(invocation.Proxy, invocation.Method.Name, invocation.Arguments, out temp))
            {
                invocation.ReturnValue = temp;
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}