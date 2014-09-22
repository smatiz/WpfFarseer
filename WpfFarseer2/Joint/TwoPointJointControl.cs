using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfFarseer
{
    public class TwoPointJointControl : BasicJointControl
    {
        public string TargetNameA
        {
            get { return (string)GetValue(TargetNameAProperty); }
            set { SetValue(TargetNameAProperty, value); }
        }
        public static readonly DependencyProperty TargetNameAProperty =
            DependencyProperty.Register("TargetNameA", typeof(string), typeof(TwoPointJointControl), new PropertyMetadata(null));

        public string TargetNameB
        {
            get { return (string)GetValue(TargetNameBProperty); }
            set { SetValue(TargetNameBProperty, value); }
        }
        public static readonly DependencyProperty TargetNameBProperty =
            DependencyProperty.Register("TargetNameB", typeof(string), typeof(TwoPointJointControl), new PropertyMetadata(null));

    }
}
