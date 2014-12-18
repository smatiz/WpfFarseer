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
        private IEnumerable<IdInfoLeaf> _infos;
        private Func<string, object> _finder;
        public IdInfoExpando(IEnumerable<IdInfo> infos, Func<string, object> finder)
        {
            _infos = new IdInfoNode("", infos).Nodes;
            _finder = finder;
        }
        IdInfoExpando(IEnumerable<IdInfoLeaf> infos)
        {
            _infos = infos;
        }

        public override object Get(string name)
        {
            var x = _infos.Where(l => l.Name == name).First();
            var y = x as IdInfoNode;
            if (y != null)
            {
                return new IdInfoExpando(y.Nodes);
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
