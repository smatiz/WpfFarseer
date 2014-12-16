using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SM.Wpf
{
    public interface ICircle
    {
        float X { get; }
        float Y { get;  }
        float Radius { get; }
    }
    public interface ISkinnedShape
    {
         //int MaxWidth { get; }
         //int MaxHeight { get; }
        Canvas Content { get; }
    }

    public interface IEllipse
    {
        float X { get; }
        float Y { get; }
        float RadiusX { get; }
        float RadiusY { get; }
    }

    public interface IPolygon
    {
         PointCollection Points { get; }
         Brush Fill { get; }
         Brush Stroke { get; }
         double StrokeThickness { get; }
         bool Convex { get; }
    }
}
