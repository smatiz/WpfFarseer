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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFarseer
{
    public class BodyControl : Canvas
    {
        Body _body;
        //List<Shape> _shapes = new List<Shape>();
        Vector2 _origin;
        RotateTransform _rotation;
       TranslateTransform _traslation;

       const float AngleSubst = 180f / (float)Math.PI;

        public BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(BodyControl), new PropertyMetadata(BodyType.Static));

        public BodyControl()
        {
            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            Loaded += (s, e) =>
            {
                var rt = this.RenderTransform;
                var gt = new TransformGroup();
                gt.Children.Add(_rotation);
                gt.Children.Add(_traslation);
                gt.Children.Add(rt);
                this.RenderTransform = gt;
                refreshVisual();
            };
        }

        public void Initialize(World world)
        {
            var p = TranslatePoint(new System.Windows.Point(0, 0), (UIElement)Parent);
            _origin = new Vector2((float)p.X, (float)p.Y);
           // _rotation.CenterX = p.X;
           // _rotation.CenterY = p.Y;

            _body = BodyFactory.CreateBody(world, Vector2.Zero);
            _body.UserData = Name;
            _body.FixtureList.AddRange(from shape in FindShapes() select ShapeToFixture(shape, _body));
            _body.BodyType = BodyType;
            _body.Position = _origin;
        }

        public void Update(World world)
        {
            refreshVisual();
            if (_body == null) return;
            var q = _body.Position - _origin;
            _traslation.X = q.X;
            _traslation.Y = q.Y;
            _rotation.Angle = _body.Rotation * AngleSubst;
        }

        private Canvas CanvasParent
        {
            get
            {
                return Parent as Canvas;
            }
        }
        private Brush GetBrush()
        {
            switch (BodyType)
            {
                case BodyType.Static:
                    return new SolidColorBrush(Colors.Black);
                case BodyType.Kinematic:
                    return new SolidColorBrush(Colors.Blue);
                case BodyType.Dynamic:
                    return new SolidColorBrush(Colors.Orange);
                default:
                    return null;
            }
        }
        private void refreshVisual()
        {
            var brush =  GetBrush();

            foreach (var shape in FindShapes())
            {
                shape.Fill = brush;
            }

        }
        private IEnumerable<Shape> FindShapes()
        {
            foreach (var x in Children)
            {
                if (x is Shape)
                {
                    yield return x as Shape;
                }
            }
        }
        private Vertices PointsToVertices(IEnumerable<System.Windows.Point> points)
        {
            return new Vertices(from p in points select new Vector2((float)p.X, (float)p.Y));
        }
        private Vertices PolygonToVertices(Polygon poly)
        {
            return PointsToVertices(from p in poly.Points select poly.TranslatePoint(p, this));
        }
        private Fixture ShapeToFixture(Shape shape, Body body)
        {
            if(shape is Polygon)
            {

                return FixtureFactory.AttachPolygon(PolygonToVertices((Polygon)shape), Const.Density, body);
            }
            else
            {
                return FixtureFactory.AttachCircle(1, 1, body);
            }
        }

        
    }
}
