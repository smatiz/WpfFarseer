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
        IPolygon _shape;
        float _scale = 1f;
        public PolygonShapeView(IContext context, IPolygon shape, float scale)
            : base(context)
        {
            _scale = scale;
            _shape = shape;
            _polygon = new Polygon();
            _polygon.Fill = _shape.Fill;
            _polygon.Stroke = _shape.Stroke;
            _polygon.StrokeThickness = _shape.StrokeThickness;
            refresh();
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
            if (_polygon.Points == null)
            {
                _polygon.Points = new PointCollection();
            }
            _polygon.Points.Clear();
            foreach (var p in _shape.Points)
            {
                _polygon.Points.Add(p.Zoomed(Context.Zoom * _scale));
            }

        }
    }
}
