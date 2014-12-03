using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBody : IDescriptor
    {
        List<IFlag> Flags { get; }

        transform2d Transform { get; }
        BodyType BodyType { get; }
        List<IShape> Shapes { get; }
    }
}
