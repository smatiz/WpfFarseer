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
    public class TriangulablePolygonShapeControl : BasicShapeControl, IPolygonsShape, IDrawable
    {
        public TriangulablePolygonShapeControl()
        {
        }

        public Polygon Polygon
        {
            get { return (Polygon)GetValue(PolygonProperty); }
            set { SetValue(PolygonProperty, value); }
        }
        public static readonly DependencyProperty PolygonProperty =
            DependencyProperty.Register("Polygon", typeof(Polygon), typeof(TriangulablePolygonShapeControl), new PropertyMetadata(null));

        public IEnumerable<IEnumerable<float2>> PolygonShapes
        {
            get
            {

                return Helper.FarseerTools.Triangulate(Polygon.Points.Select(p => new float2((float)p.X, (float)p.Y)));
            }
        }

        public UIElement UIElement
        {
            get 
            {
                return Polygon; 
            }
        }
    }
}
