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

namespace WpfFarseer
{
    public static class WpfFarseerHelper
    {
        public static Vertices ToFarseer(IEnumerable<System.Windows.Point> points)
        {
            return new Vertices(from p in points select p.ToFarseer());
        }
        public static Vertices ToFarseer(this UIElement uielement, Polygon poly)
        {
            return ToFarseer(from p in poly.Points select poly.TranslatePoint(p, uielement));
        }
        public static Fixture ToFarseer(this UIElement uielement, Shape shape, Body body)
        {
            if (shape is Polygon)
            {

                return FixtureFactory.AttachPolygon(uielement.ToFarseer((Polygon)shape), Const.Density, body);
            }
            else
            {
                return FixtureFactory.AttachCircle(1, 1, body);
            }
        }

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
