using System;
using System.Collections.Generic;

namespace MetaType
{
    public class MissingMethodCall
    {
        private readonly IEnumerator<Func<object, object[], object>> _enumerator;
        private readonly Func<Func<object, object[], object>, object> _executor;
        public static readonly object Absent = new object();

        public MissingMethodCall(
            IEnumerable<Func<object, object[], object>> methods,
            Func<Func<object, object[], object>, object> executor)
        {
            _executor = executor;
            _enumerator = methods.GetEnumerator();
        }

        public string Name { get; set; }
        public object[] Arguments { get; set; }


        public object Default()
        {
            return _enumerator.MoveNext()
                ? _executor(_enumerator.Current)
                : Absent;
        }
    }
}
