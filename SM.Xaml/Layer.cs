using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM.Xaml
{
    public class Layer : DependencyObject
    {
        public transform2d Transform
        {
            get { return (transform2d)GetValue(TransformProperty); }
            set { SetValue(TransformProperty, value); }
        }
        public static readonly DependencyProperty TransformProperty =
            DependencyProperty.Register("Transform", typeof(transform2d), typeof(Layer), new PropertyMetadata(transform2d.Null));
    }
}
