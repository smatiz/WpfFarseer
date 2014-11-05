using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SM.Wpf
{
    public class CircleShapeControl : BasicShapeControl, ICircleShape, IDrawable, IBreakableShape
    {
        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(float), typeof(CircleShapeControl), new PropertyMetadata(0f));

        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(float), typeof(CircleShapeControl), new PropertyMetadata(0f));

        public float Radius
        {
            get { return (float)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(float), typeof(CircleShapeControl), new PropertyMetadata(0f));

        private Ellipse getEllipse()
        {
            var ellipse = new System.Windows.Shapes.Ellipse() { Width = Radius * 2, Height = Radius * 2 };
            Canvas.SetLeft(ellipse, X - Radius);
            Canvas.SetTop(ellipse, Y - Radius);
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
                return getEllipse().BBox();
            }
        }


        public IEnumerable<Polygon> Polygons
        {
            get { throw new NotImplementedException(); }
        }
    }
}
