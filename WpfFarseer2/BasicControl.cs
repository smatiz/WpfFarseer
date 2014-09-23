using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfFarseer
{
    public class BasicControl : Canvas
    {
        public TimeSpan Delay
        {
            get { return (TimeSpan)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(TimeSpan), typeof(BasicControl), new PropertyMetadata(new TimeSpan(0)));
    }
}
