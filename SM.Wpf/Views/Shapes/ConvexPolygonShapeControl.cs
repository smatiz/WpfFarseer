using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.Wpf
{
    [ContentPropertyAttribute("Polygon")]
    public class ConvexPolygonShapeControl : BasicShapeControl, IPolygonShape, IDrawable, IBreakableShape
    {
        public ConvexPolygonShapeControl()
        {
        }

        public ConvexPolygonShapeControl(Polygon poly, float density)
            : this()
        {
            Polygon = poly;
            Density = density;
        }

        public Polygon Polygon
        {
            get { return (Polygon)GetValue(PolygonProperty); }
            set { SetValue(PolygonProperty, value); }
        }
        public static readonly DependencyProperty PolygonProperty =
            DependencyProperty.Register("Polygon", typeof(Polygon), typeof(ConvexPolygonShapeControl), new PropertyMetadata(null));

        public IEnumerable<float2> Points
        {
            get
            {
                return Polygon.Points.Select(p => new float2((float)p.X, (float)p.Y));
            }
        }

        public UIElement UIElement
        {
            get
            {
                return Polygon;
            }
        }

        public Rect BBox
        {
            get
            {
                return Polygon.BBox();
            }
        }



        public IEnumerable<Polygon> Polygons
        {
            get 
            {
                yield return Polygon;
            }
        }
    }
}
