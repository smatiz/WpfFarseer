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
        public static int EllipseNumberOfEdges = 4;


        public static F.Vertices ToVertices(this IEllipseShape ellipse)
        {
            return F.PolygonTools.CreateEllipse(ellipse.RadiusX, ellipse.RadiusY, EllipseNumberOfEdges);
        }
        public static FShape.PolygonShape ToShape(this IEllipseShape ellipse)
        {
            return new FShape.PolygonShape(ellipse.ToVertices(), ellipse.Density);
        }
        public static FShape.CircleShape ToShape(this ICircleShape circle)
        {
            return new FShape.CircleShape(circle.Radius, circle.Density);
        }
        public static FShape.PolygonShape ToShape(this IPolygonShape poly)
        {
            return new FShape.PolygonShape(poly.Points.ToFarseerVertices(), poly.Density);
        }

        public static IEnumerable<FShape.Shape> ToShapes(this __IShape shape)
        {
            var circle = shape as ICircleShape;
            if (circle != null)
            {
                yield return circle.ToShape();
            }

            var ellipse = shape as IEllipseShape;
            if (ellipse != null)
            {
                yield return ellipse.ToShape();
            }

            var poly = shape as IPolygonShape;
            if (poly != null)
            {
                yield return poly.ToShape();
            }

            var polys = shape as IPolygonsShape;
            if (polys != null)
            {
                foreach (var x in polys.PolygonShapes)
                {
                    yield return new FShape.PolygonShape(x.ToFarseerVertices(), polys.Density);
                }
            }
        }


        public static F.Vertices ToFarseerVertices(this IEnumerable<SM.float2> points)
        {
            return new F.Vertices(from p in points select p.ToFarseer());
        }
        public static Xna.Vector2 ToFarseer(this SM.float2 p)
        {
            return new Xna.Vector2((float)p.X, (float)p.Y);
        }
       
        public static float2 ToSM(this Xna.Vector2 p)
        {
            return new float2(p.X, p.Y);
        }

        public static List<float2> ToSM(this F.Vertices ps)
        {
            var list = new List<float2>();
            foreach (var p in ps)
            {
                list.Add(p.ToSM());
            }
            return list;
        }
        public static FShape.PolygonShape ToFarseerVertices(this IShape shape)
        {
            return new FShape.PolygonShape(shape.Points_X.ToFarseerVertices(), shape.Density_X);
        }
    }
}
