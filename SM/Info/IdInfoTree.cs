using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class IdInfoLeaf
    {
        public string Name { get; private set; }
        public IdInfoLeaf(string name)
        {
            Name = name;
        }
    }

    public class IdInfoNode : IdInfoLeaf
    {
        private IEnumerable<IdInfo> _infos;
        public IdInfoNode(string name, IEnumerable<IdInfo> infos)
            : base(name)
        {
            _infos = infos;
        }

        public IEnumerable<IdInfoLeaf> Nodes
        {
            get
            {
                var infosWithoutEmpty = _infos.Where(info => info.Parts.Count() != 0);
                var groupsByType = infosWithoutEmpty.ToLookup<IdInfo, bool>(info => info.Parts.Count() == 1);

                var leafs = groupsByType[true].ToLookup<IdInfo, string>(info => info.Parts.First()).Select(g => new IdInfoLeaf(g.Key));
                
                var groupsNode = groupsByType[false].GroupBy<IdInfo, string>(info => info.Parts.First());
                var nodes = groupsNode.Select(g => new IdInfoNode(g.Key, g.Select(item => new IdInfo(item.Parts.Skip(1).ToList()))));

                return nodes.Concat(leafs);
            }
        }
    }
}
