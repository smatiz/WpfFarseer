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
    // si occupa di gestire il dialogo tra Body e BodyControl
    // 
    public class BodyManager
    {
        const float AngleSubst = 180f / (float)Math.PI;
        private Body _body;
        private Vector2 _originalPosition;
        private BodyControl _bodyControl;

        public BodyManager(BodyControl bodyControl, Body body, Vector2 originPosition)
        {
            body.UserData = bodyControl.Name;
            body.FixtureList.AddRange(from shape in bodyControl.FindShapes() select bodyControl.ToFarseer(shape, body));
            body.BodyType = bodyControl.BodyType;
            body.Position = originPosition;


            _originalPosition = originPosition;
            _body = body;
            _bodyControl = bodyControl;
        }

        public void Update()
        {
            var q = _body.Position - _originalPosition;
            _bodyControl.Traslation.X = q.X;
            _bodyControl.Traslation.Y = q.Y;
            _bodyControl.Rotation.Angle = _body.Rotation * AngleSubst;
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

       
    }
}
