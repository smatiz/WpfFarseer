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
        public CircleShapeView(IContext context, ICircle shape, float scale)
            : base(context)
        {
            var ellipse = new System.Windows.Shapes.Ellipse() { Width = shape.Radius * context.Zoom * 2 * scale, Height = shape.Radius * context.Zoom * 2 * scale };
            ellipse.Fill = shape.Fill;
            Canvas.SetLeft(ellipse, (shape.X - shape.Radius) * context.Zoom * scale);
            Canvas.SetTop(ellipse, (shape.Y - shape.Radius) * context.Zoom * scale);
            UIElement = ellipse;
        }

         // costructor for break 
        public CircleShapeView(IContext context, Ellipse ellipse)
            : base(context)
        {
            UIElement = ellipse;
        }

        public UIElement UIElement {get; private set;}
    }
}
