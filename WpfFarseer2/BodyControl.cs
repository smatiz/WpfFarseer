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
        List<Shape> _shapes = new List<Shape>();

        public BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(BodyControl), new PropertyMetadata(BodyType.Static));

        public BodyControl()
        {
            Loaded += (s, e) =>
            {
                refreshVisual();
            };
        }

        public void Initialize(World world)
        {
            buildPhyics(world);
        }
        public void Update(World world)
        {
            refreshVisual();
            if (_body == null) return;
            setPos(_body.Position.X, _body.Position.Y);
            setRot(_body.Rotation);
        }

        private Canvas CanvasParent
        {
            get
            {
                return Parent as Canvas;
            }
        }
        private void setRot(float a)
        {
            //this.LayoutTransform = new RotateTransform(a);
            this.RenderTransform = new RotateTransform(a);
        }
        private void setPos(float x, float y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }
        private void getRectangle(out float x, out float y, out float w, out float h)
        {
            x = (float)Canvas.GetLeft(this);
            y = (float)Canvas.GetTop(this);

            w = (float)ActualWidth;
            if(w <= 0)
            {
                w = (float)CanvasParent.ActualWidth - x - (float)Canvas.GetRight(this);
            }

            h = (float)ActualHeight;
            if (h <= 0)
            {
                h = (float)CanvasParent.ActualHeight - y - (float)Canvas.GetBottom(this);
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
        private Vertices PointsToVertices(PointCollection pointCollection)
        {
            return new Vertices(from p in pointCollection select new Vector2((float)p.X, (float)p.Y));
        }
        private Fixture ShapeToFixture(Shape shape, Body body)
        {
            if(shape is Polygon)
            {

                return FixtureFactory.AttachPolygon(PointsToVertices(((Polygon)shape).Points), Const.Density, body);
            }
            else
            {
                return FixtureFactory.AttachCircle(1, 1, body);
            }
        }
        private void buildPhyics(World world)
        {
            var x = (float)Canvas.GetLeft(this);
            var y = (float)Canvas.GetTop(this);
            _body = BodyFactory.CreateBody(world, new Vector2(x, y));
            _body.FixtureList.AddRange(from shape in FindShapes() select ShapeToFixture(shape, _body));
            _body.BodyType = BodyType;
            _body.Position = new Vector2(x, y);
        }
    }
}
