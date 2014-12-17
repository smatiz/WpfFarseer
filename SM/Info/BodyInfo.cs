using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class BodyInfo
    {
        public IEnumerable<IShape> Shapes { get; private set; }
        public IEnumerable<IFlag> Flags { get; private set; }

        public transform2d Transform { get; private set; }
        public BodyType BodyType { get; private set; }
    }
}
