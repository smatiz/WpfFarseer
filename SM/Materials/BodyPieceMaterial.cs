using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class BodyPieceMaterial
    {
        public IEnumerable<float2> Polygon { get; set; }
        public IBodyMaterial BodyMaterial { get; set; }
    }
}
