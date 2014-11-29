using SM.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.WpfView
{
    public class PolygonShapeView : BasicShapeView, IDrawable
    {
        Polygon _polygon;
        public PolygonShapeView(IContext context, IPolygon shape)
            : base(context)
        {
            _polygon = new Polygon();
            _polygon.Points = shape.Points;
            _polygon.Fill = shape.Fill;
            _polygon.Stroke = shape.Stroke;
            _polygon.StrokeThickness = shape.StrokeThickness;
           
            UIElement = _polygon;
        }

        // costructor for break 
        public PolygonShapeView(IContext context, Polygon polygon)
            : base(context)
        {
            _polygon = polygon;
            UIElement = _polygon;
        }

        public UIElement UIElement { get; private set; }

        void refresh()
        {
            if (_polygon.Points != null)
            {
                _polygon.Points.Clear();
                foreach (var p in _polygon.Points)
                {
                    _polygon.Points.Add(p.Zoomed(Context.Zoom));
                }
            }
        }
    }
}
