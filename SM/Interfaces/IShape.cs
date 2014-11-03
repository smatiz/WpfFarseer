using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IShape
    {
        float Density { get; }
    }
    public interface IPolygonShape : IShape
    {
        IEnumerable<float2> Points { get; }
    }
    public interface ICircleShape : IShape
    {
        float X { get; }
        float Y { get; }
        float Radius { get; }
    }
    public interface IEllipseShape : IShape
    {
        float X { get; }
        float Y { get; }
        float RadiusX { get; }
        float RadiusY { get; }
    }
    public interface IPolygonsShape : IShape
    {
        IEnumerable<IEnumerable<float2>> PolygonShapes { get; }
    }

}
