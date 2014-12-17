using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
   // [TypeConverter(typeof(IdInfoConverter))]
    public class IdInfo : List<string>
    {
         IdInfo() { }

         public static IdInfo Null { get { return new IdInfo(); } }


         public static implicit operator IdInfo(string s)
         {
             var idinfo = new IdInfo();
             idinfo.Add(s);
             return idinfo;
         }
         //public static explicit operator IdInfo(string s)
         //{
         //    var idinfo = new IdInfo();
         //    idinfo.Add(s);
         //    return idinfo;
         //}

         public static IdInfo operator +(IdInfo idinfo1, IdInfo idinfo2)
         {
             var idinfo = new IdInfo();
             if (idinfo1 != null)
             {
                 foreach (var s in idinfo1)
                 {
                     idinfo.Add(s);
                 }
             }
             if (idinfo2 != null)
             {
                 foreach (var s in idinfo2)
                 {
                     idinfo.Add(s);
                 }
             }
             return idinfo;
         }

         public override bool Equals(object obj)
         {
             if (obj == this) return true;
             var obj2 = obj as IdInfo;
             if (obj2 == null) return false;
             if (obj2.Count != Count) return false;
             var ts = this.Zip(obj2, (x, y) => new Tuple<string, string>(x, y));
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
                 foreach (var s in this)
                 {
                     hash = hash * 31 + s.GetHashCode();
                 }
                 return hash;
             }
         }
    }

}
