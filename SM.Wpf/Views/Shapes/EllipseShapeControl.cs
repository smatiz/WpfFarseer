using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.Wpf
{
    public class EllipseShapeControl : BasicShapeControl, IEllipseShape, IDrawable, IBreakableShape
    {
        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(float), typeof(EllipseShapeControl), new PropertyMetadata(0f));

        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(float), typeof(EllipseShapeControl), new PropertyMetadata(0f));

        public float RadiusX
        {
            get { return (float)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }
        public static readonly DependencyProperty RadiusXProperty =
            DependencyProperty.Register("RadiusX", typeof(float), typeof(EllipseShapeControl), new PropertyMetadata(0f));


        public float RadiusY
        {
            get { return (float)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }
        public static readonly DependencyProperty RadiusYProperty =
            DependencyProperty.Register("RadiusY", typeof(float), typeof(EllipseShapeControl), new PropertyMetadata(0f));


        private Ellipse getEllipse()
        {
            var ellipse = new System.Windows.Shapes.Ellipse() { Width = RadiusX * Context.Zoom * 2, Height = RadiusY * Context.Zoom * 2 };
            Canvas.SetLeft(ellipse, (X - RadiusX) * Context.Zoom);
            Canvas.SetTop(ellipse, (Y - RadiusY) * Context.Zoom);
            return ellipse;
        }

        public UIElement UIElement
        {
            get
            {
                return getEllipse();
            }
        }
        public Rect BBox
        {
            get
            {
                return new Rect(X - RadiusX, Y - RadiusY, RadiusX, RadiusY);
            }
        }


        public IEnumerable<PointCollection> PointCollections
        {
            get { throw new NotImplementedException(); }
        }

        public IContext Context
        {
            private get;
            set;
        }
    }
}
