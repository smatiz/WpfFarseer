using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFarseer
{
    public class BodyManager
    {
        const float AngleSubst = 180f / (float)Math.PI;
        private Body _body;
        Vector2 _originalPosition;

        public BodyManager(Body body, Vector2 originPosition)
        {
            _originalPosition = originPosition;
            _body = body;
        }

        public void Update(System.Windows.Media.TranslateTransform _traslation, System.Windows.Media.RotateTransform _rotation)
        {
            var q = _body.Position - _originalPosition;
            _traslation.X = q.X;
            _traslation.Y = q.Y;
            _rotation.Angle = _body.Rotation * AngleSubst;
        }

        public void Draw(System.Windows.Media.DrawingContext drawingContext)
        {
            foreach (var f in _body.FixtureList)
            {
                if (f.Shape is PolygonShape)
                {
                    var ps = (PolygonShape)f.Shape;


                    var vs = (from x in ps.Vertices select _body.GetWorldPoint(x)).ToArray();


                    var streamGeometry = new System.Windows.Media.StreamGeometry();
                    using (var geometryContext = streamGeometry.Open())
                    {

                        geometryContext.BeginFigure(vs[0].ToWpf(), true, true);
                        var points = new System.Windows.Media.PointCollection();
                        for (int i = 1; i < vs.Length; i++)
                        {
                            points.Add(vs[i].ToWpf());
                        }
                        geometryContext.PolyLineTo(points, true, true);
                    }

                    drawingContext.DrawGeometry(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 250, 0, 0)), new System.Windows.Media.Pen(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black), 2), streamGeometry);



                    drawingContext.DrawRectangle(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 0, 0, 250)), new System.Windows.Media.Pen(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black), 3), new System.Windows.Rect(_body.Position.ToWpf(), new System.Windows.Size(10, 10)));
                    //ps.Vertices[0].
                }
            }



        }

        public static JointManager CreateAngleJoint(World _world, BodyManager b1, BodyManager b2)
        {
            return new JointManager(JointFactory.CreateAngleJoint(_world, b1._body, b2._body));
        }


        public static JointManager CreateRopeJoint(World _world, BodyManager b1, BodyManager b2, Vector2 v1, Vector2 v2 )
        {
            return new JointManager(JointFactory.CreateRopeJoint(_world, b1._body, b2._body, v1, v2, true));
        }
    }
}
