using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfFarseer
{
    public class WpfDebugView
    {
        private static WpfDebugView _instance = new WpfDebugView();
        public static WpfDebugView Instance
        {
            get
            {
                return _instance;
            }
        }


        private List<Body> _bodies = new List<Body>();
        /*public World World
        {
            get;
            set;
        }*/

        public void Add(Body body)
        {
            _bodies.Add(body);
        }

        


        Vector2 trans(Body b, Vector2 v)
        {

            return b.GetWorldPoint(v);

            var m = new MathNet.Numerics.LinearAlgebra.Single.DenseMatrix(2,2, new float[]{ 
                (float)Math.Cos(b.Rotation * Math.PI / 180f),  -(float)Math.Sin(b.Rotation * Math.PI / 180f),
                 (float)Math.Sin(b.Rotation * Math.PI / 180f),  (float)Math.Cos(b.Rotation * Math.PI / 180f)
            });
            var v2 = b.GetWorldPoint(v) ;
            var x = m * new MathNet.Numerics.LinearAlgebra.Single.DenseVector(new float[] { v2.X, v2.Y });
            return new Vector2(x[0], x[1]) + b.Position;
        }
       

        public void Draw(DrawingContext drawingContext)
        {
            foreach(var b in _bodies)
            {
                foreach (var f in b.FixtureList)
                {
                    if (f.Shape is PolygonShape)
                    {
                        var ps = (PolygonShape)f.Shape;



                        var vs = (from x in ps.Vertices select trans(b, x)).ToArray();


                        StreamGeometry streamGeometry = new StreamGeometry();
                        using (StreamGeometryContext geometryContext = streamGeometry.Open())
                        {

                            geometryContext.BeginFigure(vs[0].ToWpf(), true, true);
                            PointCollection points = new PointCollection();
                            for (int i = 1; i < vs.Length; i++)
                            {
                                points.Add(vs[i].ToWpf());
                            }
                            geometryContext.PolyLineTo(points, true, true);
                        }

                        drawingContext.DrawGeometry(new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 250, 0, 0)), new Pen(new SolidColorBrush(Colors.Black), 2), streamGeometry);



                        drawingContext.DrawRectangle(new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 0, 0, 250)), new Pen(new SolidColorBrush(Colors.Black), 3), new System.Windows.Rect(b.Position.ToWpf(), new System.Windows.Size(10, 10)));
                        //ps.Vertices[0].
                    }
                }


                
            }
        }
    }
}
