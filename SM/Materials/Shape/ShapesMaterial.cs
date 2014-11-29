using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class ShapesMaterial
    {
        public IEnumerable<CircleMaterial> Circles { get; private set; }
        public IEnumerable<PolygonMaterial> Polygons { get; private set; }

        public ShapesMaterial(IEnumerable<CircleMaterial> circles, IEnumerable<PolygonMaterial> polygons)
        {
            Circles = circles;
            Polygons = polygons;
        }
    }
}
