using System.Collections.Generic;
using System.Linq;
using F = FarseerPhysics.Common;
using W = System.Windows;
using Xna = Microsoft.Xna.Framework;
using WShape = System.Windows.Shapes;
using FShape = FarseerPhysics.Collision.Shapes;

namespace WpfFarseer
{
    public static class WpfFarseerHelper
    {
        private static F.Vertices ToFarseerVertices(this W.UIElement uielement, WShape.Polygon poly)
        {
            return new F.Vertices(from p in poly.Points select poly.TranslatePoint(p, uielement).ToFarseer());
        }
        public static F.Vertices ToFarseerVertices(this W.UIElement uielement, WShape.Shape shape)
        {
            if (shape is W.Shapes.Polygon)
            {
                return uielement.ToFarseerVertices(shape as WShape.Polygon);
            }

            return null;
        }
        public static FShape.Shape ToFarseerShape(this W.UIElement uielement, WShape.Shape shape, float density = SM.Farseer.Const.Density)
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
        public static W.Shapes.Polygon ToWpf(this F.Vertices ps)
        {
            var poly = new W.Shapes.Polygon();
            foreach(var p in ps)
            {
                poly.Points.Add(p.ToWpf());
            }
            return poly;
        }

        public static Xna.Vector2 GetOrigin(this W.FrameworkElement elem)
        {
            return WpfFarseerHelper.ToFarseer(elem.TranslatePoint(new System.Windows.Point(0, 0), (System.Windows.UIElement)elem.Parent));
        }
    }
}
