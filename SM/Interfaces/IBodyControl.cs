using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBodyControl : IIdentifiable
    {
        void Set(float x, float y, float a);
        BodyType BodyType { get; }
        IEnumerable<IPointControl> FlagsPoints { get; }
        IEnumerable<IShape> Shapes_X { get; }
    }
}
