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
        float X { get; }
        float Y { get; }
        float Radius { get; }
    }
    public interface IEllipseShape : __IShape
    {
        float X { get; }
        float Y { get; }
        float RadiusX { get; }
        float RadiusY { get; }
    }
    public interface IPolygonsShape : __IShape
    {
        IEnumerable<IEnumerable<float2>> PolygonShapes { get; }
    }

}
