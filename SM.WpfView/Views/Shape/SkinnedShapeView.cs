using SM.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace SM.WpfView
{
    public class SkinnedShapeView : BasicShapeView, IDrawable
    {
        Canvas _canvas = new Canvas() { Background = new SolidColorBrush(Colors.Transparent) };//, Width = 1000, Height = 1000 };

        public SkinnedShapeView(IContext context, ISkinnedShape shape, float scale)
            : base(context)
        {
            _canvas.Children.Clear();
            var parent = shape.Content.Parent as Canvas;
            if (parent != null)
            {
                parent.Children.Remove(shape.Content);
            }
            _canvas.Children.Add(shape.Content);
            _canvas.RenderTransform = new ScaleTransform(Context.Zoom * scale, Context.Zoom * scale);
        }

        public UIElement UIElement
        {
            get
            {
                return _canvas;
            }
        }
    }
}
