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
    public class CircleShapeView : BasicShapeView, IDrawable
    {
        public CircleShapeView(IContext context, ICircle shape)
            : base(context)
        {
            var ellipse = new System.Windows.Shapes.Ellipse() { Width = shape.Radius * context.Zoom * 2, Height = shape.Radius * context.Zoom * 2 };
            Canvas.SetLeft(ellipse, (shape.X - shape.Radius) * context.Zoom);
            Canvas.SetTop(ellipse, (shape.Y - shape.Radius) * context.Zoom);
            UIElement = ellipse;
        }

        public UIElement UIElement {get; private set;}
    }
}
