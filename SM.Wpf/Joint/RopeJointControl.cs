using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using SM;

namespace SM.Wpf
{
    public class RopeJointControl : TwoPointJointControl, IRopeJointControl, IRopeJointView
    {
        public float MaxLength
        {
            get { return (float)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(float), typeof(RopeJointControl), new PropertyMetadata(-1f));

        public float MaxLengthFactor
        {
            get { return (float)GetValue(MaxLengthFactorProperty); }
            set { SetValue(MaxLengthFactorProperty, value); }
        }
        public static readonly DependencyProperty MaxLengthFactorProperty =
            DependencyProperty.Register("MaxLengthFactor", typeof(float), typeof(RopeJointControl), new PropertyMetadata(-1f));



    }
}
