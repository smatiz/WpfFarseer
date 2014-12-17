using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBody : IEntity, ITransformable, IFlaggable
    {
        BodyType BodyType { get; }
        List<IShape> Shapes { get; }
    }
}
