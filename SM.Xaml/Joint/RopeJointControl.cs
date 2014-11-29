using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using SM;
using System.Windows.Media;

namespace SM.Xaml
{
    public class RopeJointControl : BasicJointControl, IRopeJoint
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


        public string TargetFlagIdA
        {
            get { return (string)GetValue(TargetFlagIdAProperty); }
            set { SetValue(TargetFlagIdAProperty, value); }
        }
        public static readonly DependencyProperty TargetFlagIdAProperty =
            DependencyProperty.Register("TargetFlagIdA", typeof(string), typeof(RopeJointControl), new PropertyMetadata(null));

        public string TargetFlagIdB
        {
            get { return (string)GetValue(TargetFlagIdBProperty); }
            set { SetValue(TargetFlagIdBProperty, value); }
        }
        public static readonly DependencyProperty TargetFlagIdBProperty =
            DependencyProperty.Register("TargetFlagIdB", typeof(string), typeof(RopeJointControl), new PropertyMetadata(null));


    }
}
