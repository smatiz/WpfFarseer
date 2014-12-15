using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface ITransformable
    {
        transform2d Transform { get;  set; }
    }


    public interface IBody : IEntity, ITransformable
    {
        List<IFlag> Flags { get; }

        BodyType BodyType { get; }
        List<IShape> Shapes { get; }
    }
}
