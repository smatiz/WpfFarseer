using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class BodyInfo
    {
        public IdInfo Id { get; set; }
        public IEnumerable<IShape> Shapes { get; set; }
        public IEnumerable<IFlag> Flags { get; set; }

        public transform2d Transform { get; set; }
        public BodyType BodyType { get; set; }
    }
}
