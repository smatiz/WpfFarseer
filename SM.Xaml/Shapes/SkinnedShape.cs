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

namespace SM.Xaml
{
    [ContentPropertyAttribute("Content")]
    public class SkinnedShape : BasicShape, ISkinnedShape
    {
        public Canvas Content
        {
            get { return (Canvas)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(Canvas), typeof(SkinnedShape), new PropertyMetadata(null));

        public double PrecisionZoom
        {
            get { return (double)GetValue(PrecisionZoomProperty); }
            set { SetValue(PrecisionZoomProperty, value); }
        }
        public static readonly DependencyProperty PrecisionZoomProperty =
            DependencyProperty.Register("PrecisionZoom", typeof(double), typeof(SkinnedShape), new PropertyMetadata(1.0));
    }
}
