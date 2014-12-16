using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    // deve generare un nome che sia accettabile per il codegenerator
    // e che sia unico
    public class Name
    {
        Name _parent;
        List<Name> _children = new List<Name>();
        const string PreAutoGenerate = "_";
        string _name;
        public string Id 
        {
            get
            {
                List<string> names = new List<string>();
                var n = this;
                while (n != null)
                {
                    names.Add(n._name);
                    n = n._parent;
                }
                names.Reverse();
                return names.Aggregate((s1, s2) => String.Format("{0}.{1}"));
            }
        }
        static int i = 0;
        private static string GetAutoGenerateName()
        {
            return string.Format("{0}{1}", PreAutoGenerate, i++);
        }
       
        public Name(Name parent)
            : this(parent, Name.GetAutoGenerateName())
        {
        }

        public Name(Name parent, string name)
        {
            _name = name;
            _parent = parent;
            if (ContainsChildrenWithName(_name))
            {
                throw new Exception(String.Format("There is already a children of {0} with name {1}", Id, _name));
            }
            _parent._children.Add(this);
            //Id = string.Format("{0}{1}", parent._name, _name);
        }

        private bool ContainsChildrenWithName(string name)
        {
            return _children.Where(c => c._name == name).Count() > 0;
        }
    }
}
