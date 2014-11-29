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
        public int MaxWidth
        {
            get { return (int)GetValue(MaxWidthProperty); }
            set { SetValue(MaxWidthProperty, value); }
        }
        public static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.Register("MaxWidth", typeof(int), typeof(SkinnedShape), new PropertyMetadata(1000));

        public int MaxHeight
        {
            get { return (int)GetValue(MaxHeightProperty); }
            set { SetValue(MaxHeightProperty, value); }
        }
        public static readonly DependencyProperty MaxHeightProperty =
            DependencyProperty.Register("MaxHeight", typeof(int), typeof(SkinnedShape), new PropertyMetadata(1000));

        public Canvas Content
        {
            get { return (Canvas)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(Canvas), typeof(SkinnedShape), new PropertyMetadata(null));
    }
}
