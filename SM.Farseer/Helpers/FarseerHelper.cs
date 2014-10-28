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
        public static F.Vertices ToFarseerVertices(this IEnumerable<SM.float2> points)
        {
            return new F.Vertices(from p in points select p.ToFarseer());
        }
        public static Xna.Vector2 ToFarseer(this SM.float2 p)
        {
            return new Xna.Vector2((float)p.X, (float)p.Y);
        }
       
        public static float2 ToFarseer(this Xna.Vector2 p)
        {
            return new float2(p.X, p.Y);
        }
        public static FShape.PolygonShape ToFarseerVertices(this IShape shape)
        {
            return new FShape.PolygonShape(shape.Points_X.ToFarseerVertices(), shape.Density_X);
        }
    }
}
