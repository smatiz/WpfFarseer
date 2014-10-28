using System.Collections.Generic;
using System.Linq;
using F = FarseerPhysics.Common;
using W = System.Windows;
using Xna = Microsoft.Xna.Framework;
using WShape = System.Windows.Shapes;
using FShape = FarseerPhysics.Collision.Shapes;
using SM;


namespace SM.WpfFarseer
{
    public static class WpfFarseerHelper
    {
        private static F.Vertices ToFarseerVertices(this W.UIElement uielement, WShape.Polygon poly)
        {
            return new F.Vertices(from p in poly.Points select poly.TranslatePoint(p, uielement).ToFarseer());
        }

        public static F.Vertices ToFarseerVertices(this W.Media.PointCollection points)
        {
            return new F.Vertices(from p in points select p.ToFarseer());
        }
        public static F.Vertices ToFarseerVertices(this IEnumerable<SM.float2> points)
        {
            return new F.Vertices(from p in points select p.ToFarseer());
        }
        public static Xna.Vector2 ToFarseer(this SM.float2 p)
        {
            return new Xna.Vector2((float)p.X, (float)p.Y);
        }
        public static F.Vertices ToFarseerVertices(this W.UIElement uielement, WShape.Shape shape)
        {
            if (shape is W.Shapes.Polygon)
            {
                return uielement.ToFarseerVertices(shape as WShape.Polygon);
            }

            return null;
        }
        public static FShape.Shape ToFarseerShape(this W.UIElement uielement, WShape.Shape shape, float density = Consts.Density)
        {
            if (shape is W.Shapes.Polygon)
            {
                return new FShape.PolygonShape(uielement.ToFarseerVertices(shape as WShape.Polygon), density);
            }
            return null;
        }
        public static Xna.Vector2 ToFarseer(this W.Point p)
        {
            return new Xna.Vector2((float)p.X, (float)p.Y);
        }
        public static W.Point ToWpf(this Xna.Vector2 p)
        {
            return new W.Point(p.X, p.Y);
        }
        public static W.Media.PointCollection ToWpf(this F.Vertices ps)
        {
            var pointCollection = new W.Media.PointCollection();
            foreach(var p in ps)
            {
                pointCollection.Add(p.ToWpf());
            }
            return pointCollection;
        }

        public static Xna.Vector2 GetOrigin(this W.FrameworkElement elem)
        {
            return WpfFarseerHelper.ToFarseer(elem.TranslatePoint(new System.Windows.Point(0, 0), (System.Windows.UIElement)elem.Parent));
        }
    }
}
