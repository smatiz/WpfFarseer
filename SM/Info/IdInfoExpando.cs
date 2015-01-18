using SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class IdInfoExpando : BasicExpando
    {

        IdInfoNode _root;
        private Func<string, object> _finder;
        public IdInfoExpando(Func<string, object> finder)
        {
            _root = new IdInfoNode("");
            //_infos = new List<IdInfo>();
            _finder = finder;
        }
        IdInfoExpando(IdInfoNode root)
        {
            _root = root;
        }

        public void Clear()
        {
            _root.Clear();
        }
        public void Add(IEnumerable<IdInfo> idinfos)
        {
            _root.Add(idinfos);
        }

        public override object Get(string name)
        {
            var x = _root.Find(name);// _infos.Where(l => l.Name == name).First();
            var y = x as IdInfoNode;
            if (y != null)
            {
                return new IdInfoExpando(y);
            }
            else
            {
                return _finder(name);
            }

        }

        public override void Set(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
