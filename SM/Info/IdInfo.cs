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
        readonly List<string> _list;
        IdInfo() 
        { 
            _list = new List<string>();
        }
        IdInfo(List<string> list)
        { 
            _list = list;
        }

        public IdInfo Parent
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 0; i < _list.Count - 1; i++)
                {
                    list.Add(_list[i]);
                }
                return new IdInfo(list);
            }
        }

         public static IdInfo Null { get { return new IdInfo(); } }
         public static implicit operator IdInfo(string id)
         {
             var idinfo = new IdInfo();
             if (id != null)
             {
                 var ss = id.Split(Separator);
                 foreach (var s in ss)
                 {
                     idinfo._list.Add(s);
                 }
             }
             return idinfo;
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
             var idinfo = new IdInfo();
             if (idinfo1 != null)
             {
                 foreach (var s in idinfo1._list)
                 {
                     idinfo._list.Add(s);
                 }
             }
             if (idinfo2 != null)
             {
                 foreach (var s in idinfo2._list)
                 {
                     idinfo._list.Add(s);
                 }
             }
             return idinfo;
         }

         public override bool Equals(object obj)
         {
             if (!(obj is IdInfo)) return false;
             var obj2 = (IdInfo)obj;
             if (obj2._list.Count != _list.Count) return false;
             var ts = _list.Zip(obj2._list, (x, y) => new Tuple<string, string>(x, y));
             foreach(var t in ts)
             {
                 if(t.Item1 != t.Item2)
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
             return _list.Aggregate((s1, s2) => string.Format("{0}{1}{2}", s1,Separator, s2));
         }

    }

}
