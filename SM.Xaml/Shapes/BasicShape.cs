using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM.Xaml
{
    public abstract class BasicShape : DependencyObject, IShape
    {
        public float Density
        {
            get { return (float)GetValue(DensityProperty); }
            set { SetValue(DensityProperty, value); }
        }
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.Register("Density", typeof(float), typeof(BasicShape), new PropertyMetadata(1f));
    }
}