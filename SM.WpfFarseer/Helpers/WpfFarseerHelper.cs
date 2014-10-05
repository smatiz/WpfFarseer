using SM.Farseer;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WpfFarseer
{
    using FShape = FarseerPhysics.Collision.Shapes;
    public static class WpfFarseerHelper
    {
        public static Vertices ToFarseer(this IEnumerable<System.Windows.Point> points)
        {
            return new Vertices(from p in points select p.ToFarseer());
        }
        public static Vertices ToFarseer(this UIElement uielement, Polygon poly)
        {
            return ToFarseer(from p in poly.Points select poly.TranslatePoint(p, uielement));
        }
        public static FShape.Shape ToFarseer(this Shape shape, float density = Const.Density)
        {
            if (shape is Polygon)
            {
                var poly = shape as Polygon;
                return new FShape.PolygonShape(poly.Points.ToFarseer(), density);
            }

            return null;
        }

        //public static FShape.Shape ToFarseer(this IEnumerable<Polygon> polys)
        //{
        //    return new FShape.ChainShape(from poly in polys select poly.Points.ToFarseer(), true);
        //}
        
        public static Vector2 ToFarseer(this System.Windows.Point p)
        {
            return new Vector2((float)p.X, (float)p.Y);
        }
        public static System.Windows.Point ToWpf(this Vector2 p)
        {
            return new System.Windows.Point(p.X, p.Y);
        }
    }
}
