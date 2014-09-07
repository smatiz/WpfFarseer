using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MetaType
{
    public class InstanceMetaObject : DynamicMetaObject
    {
        private static readonly MethodInfo ExtendedObjectProviderGetExtendedObject = typeof(IExtendedObjectProvider).GetMethod("GetExtendedObject");
        private static readonly MethodInfo ExtendedObjectTryGet = typeof(ExtendedObject).GetMethod("TryGet");
        private static readonly MethodInfo ExtendedObjectSet = typeof(ExtendedObject).GetMethod("Set");
        private static readonly MethodInfo ExtendedObjectTryInvokeMember = typeof(ExtendedObject).GetMethod("TryInvokeMember");

        public InstanceMetaObject(Expression expression, object value)
            : base(expression, BindingRestrictions.Empty, value)
        {
        }

        private Expression BindInstance()
        {
            return Expression.Call(
                Expression.Convert(Expression, typeof(IExtendedObjectProvider)),
                ExtendedObjectProviderGetExtendedObject);
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            var fallback = base.BindGetMember(binder);
            if (fallback.Expression.NodeType == ExpressionType.Throw)
            {
                var temp = Expression.Parameter(typeof(object), null);

                // [instance].TryGet(name,out temp) ? temp : [fallback]
                return new DynamicMetaObject(
                    Expression.Block(
                        new[] { temp },
                        Expression.Condition(
                            Expression.Call(
                                BindInstance(),
                                ExtendedObjectTryGet,
                                Expression,
                                Expression.Constant(binder.Name),
                                temp),
                            temp,
                            fallback.Expression)),
                    BindingRestrictions.GetTypeRestriction(Expression, Value.GetType()).Merge(fallback.Restrictions));
            }
            return fallback;
        }

        public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
        {
            var fallback = base.BindSetMember(binder, value);
            if (fallback.Expression.NodeType == ExpressionType.Throw &&
                Value.GetType().GetProperty(binder.Name) == null)
            {
                return new DynamicMetaObject(
                    Expression.Call(
                        BindInstance(),
                        ExtendedObjectSet,
                        Expression.Constant(binder.Name),
                        Expression.Convert(value.Expression, typeof(object))),
                    BindingRestrictions.GetTypeRestriction(Expression, Value.GetType()));
            }
            return fallback;
        }

        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        {
            var fallback = base.BindInvokeMember(binder, args);
            if (fallback.Expression.NodeType == ExpressionType.Throw &&
                Value.GetType().GetMethod(binder.Name) == null)
            {
                var temp = Expression.Parameter(typeof(object), null);

                var restrictions =
                    BindingRestrictions.GetTypeRestriction(Expression, Value.GetType()).Merge(fallback.Restrictions);

                restrictions = args.Aggregate(restrictions, (r, arg) => r.Merge(arg.Restrictions));

                // [instance].TryInvokeMember(proxy,name,args,out temp) ? temp : [fallback]
                return new DynamicMetaObject(
                    Expression.Block(
                        new[] { temp },
                        Expression.Condition(
                            Expression.Call(
                                BindInstance(),
                                ExtendedObjectTryInvokeMember,
                                Expression,
                                Expression.Constant(binder.Name),
                                Expression.NewArrayInit(typeof(object), args.Select(arg => Expression.Convert(arg.Expression, typeof(object)))),
                                temp),
                            temp,
                            fallback.Expression)),
                    restrictions);
            }
            return fallback;
        }
    }
}