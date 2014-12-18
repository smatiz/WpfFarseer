
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    // [TypeConverter(typeof(IdInfoConverter))]
    public class IdInfo
    {
        const char Separator = '.';
        readonly IEnumerable<string> _list;
        IdInfo()
        {
            _list = new string[0];
        }
        public IdInfo(IEnumerable<string> list)
        {
            _list = list.ToArray();
        }

        public IdInfo Parent
        {
            get
            {
                return new IdInfo(_list.Reverse().Skip(1).Reverse());
            }
        }

        public IEnumerable<string> Parts
        {
            get
            {
                return _list;
            }
        }

        public static IdInfo Null { get { return new IdInfo(); } }
        public static implicit operator IdInfo(string id)
        {
            if (id != null)
            {
                return new IdInfo(id.Split(Separator));
            }
            return new IdInfo();
        }
        public static bool operator ==(IdInfo idinfo1, IdInfo idinfo2)
        {
            return idinfo1.Equals(idinfo2);
        }
        public static bool operator !=(IdInfo idinfo1, IdInfo idinfo2)
        {
            return !idinfo1.Equals(idinfo2);
        }
        public static IdInfo operator +(IdInfo idinfo1, IdInfo idinfo2)
        {
            var list = new List<string>();
            if (idinfo1 != null)
            {
                foreach (var s in idinfo1._list)
                {
                    list.Add(s);
                }
            }
            if (idinfo2 != null)
            {
                foreach (var s in idinfo2._list)
                {
                    list.Add(s);
                }
            }
            return new IdInfo(list);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IdInfo)) return false;
            var obj2 = (IdInfo)obj;
            if (obj2._list.Count() != _list.Count()) return false;
            var ts = _list.Zip(obj2._list, (x, y) => new Tuple<string, string>(x, y));
            foreach (var t in ts)
            {
                if (t.Item1 != t.Item2)
                {
                    return false;
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var s in _list)
                {
                    hash = hash * 31 + s.GetHashCode();
                }
                return hash;
            }
        }

        public override string ToString()
        {
            return _list.Aggregate((s1, s2) => string.Format("{0}{1}{2}", s1, Separator, s2));
        }

    }

}