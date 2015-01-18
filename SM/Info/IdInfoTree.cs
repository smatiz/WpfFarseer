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
        List<IdInfoLeaf> _children;
        public IdInfoNode(string name)
            : base(name)
        {
            _children = new List<IdInfoLeaf>();
        }

        IdInfoNode(string name, List<IdInfo> infos)
            : this(name)
        {
            _children.AddRange(GetNodes(infos));
        }

        public void Add(IEnumerable<IdInfo> infos)
        {
            _children.AddRange(GetNodes(infos));
        }

        public IdInfoLeaf Find(string name)
        {
            return _children.Where(n => n.Name == name).First();
        }

        private static IEnumerable<IdInfoLeaf> GetNodes(IEnumerable<IdInfo> infos)
        {
            var infosWithoutEmpty = infos.Where(info => info.Parts.Count() != 0);
            var groupsByType = infosWithoutEmpty.ToLookup<IdInfo, bool>(info => info.Parts.Count() == 1);

            var leafs = groupsByType[true].ToLookup<IdInfo, string>(info => info.Parts.First()).Select(g => new IdInfoLeaf(g.Key));

            var groupsNode = groupsByType[false].GroupBy<IdInfo, string>(info => info.Parts.First());
            var nodes = groupsNode.Select(g => new IdInfoNode(g.Key, g.Select(item => new IdInfo(item.Parts.Skip(1).ToList())).ToList()));

            return nodes.Concat(leafs);
        }

        internal void Clear()
        {
            _children.Clear();
        }
    }
}
