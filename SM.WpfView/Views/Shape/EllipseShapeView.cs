using SM.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.WpfView
{
    public class EllipseShapeView : BasicShapeView, IDrawable
    {
        public EllipseShapeView(IContext context, IEllipse shape, float scale)
            : base(context)
        {
            var ellipse = new System.Windows.Shapes.Ellipse() { Width = shape.RadiusX * context.Zoom * scale * 2, Height = shape.RadiusY * context.Zoom * scale * 2 };
            Canvas.SetLeft(ellipse, (shape.X - shape.RadiusX) * context.Zoom * scale);
            Canvas.SetTop(ellipse, (shape.Y - shape.RadiusY) * context.Zoom * scale);
            UIElement = ellipse;
        }

        public UIElement UIElement { get; private set; }
    }
}
