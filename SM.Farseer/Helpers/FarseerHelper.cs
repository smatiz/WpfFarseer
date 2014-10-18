using System.Collections.Generic;
using System.Linq;
using F = FarseerPhysics.Common;
using W = System.Windows;
using Xna = Microsoft.Xna.Framework;
using FShape = FarseerPhysics.Collision.Shapes;


namespace SM.Farseer
{
    public static class WpfFarseerHelper
    {
        public static F.Vertices ToFarseerVertices(this IEnumerable<SM.IVector2> points)
        {
            return new F.Vertices(from p in points select p.ToFarseer());
        }
        public static Xna.Vector2 ToFarseer(this SM.IVector2 p)
        {
            return new Xna.Vector2((float)p.X, (float)p.Y);
        }
        public static FShape.PolygonShape ToFarseerVertices(this IShapeView shape)
        {
            return new FShape.PolygonShape(shape.Points_X.ToFarseerVertices(), shape.Density_X);
        }
    }
}
