using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public abstract class BodyPieceMaterial
    {
        public IBodyMaterial BodyMaterial { get; set; }
    }

    public class PolygonPieceMaterial : BodyPieceMaterial
    {
        public IEnumerable<float2> Polygon { get; set; }
    }

    public class CirclePieceMaterial : BodyPieceMaterial
    {
        public Circle2 Circle { get; set; }
    }
}
