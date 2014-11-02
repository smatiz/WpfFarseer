using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface __IShape
    {
        float Density { get; }
    }
    public interface IPolygonShape : __IShape
    {
        IEnumerable<float2> Points { get; }
    }
    public interface ICircleShape : __IShape
    {
        float Radius { get; }
    }
    public interface IPolygonsShape : __IShape
    {
        IEnumerable<IEnumerable<float2>> PolygonShapes { get; }
    }

    public interface IShape
    {
        IEnumerable<float2> Points_X { get; }
        float Density_X { get; }
    }
}
