using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfFarseer
{
    public class RopeJointControl : Canvas
    {
        public string TargetNameA
        {
            get { return (string)GetValue(TargetNameAProperty); }
            set { SetValue(TargetNameAProperty, value); }
        }
        public static readonly DependencyProperty TargetNameAProperty =
            DependencyProperty.Register("TargetNameA", typeof(string), typeof(RopeJointControl), new PropertyMetadata(null));
        
        public string TargetNameB
        {
            get { return (string)GetValue(TargetNameBProperty); }
            set { SetValue(TargetNameBProperty, value); }
        }
        public static readonly DependencyProperty TargetNameBProperty =
            DependencyProperty.Register("TargetNameB", typeof(string), typeof(RopeJointControl), new PropertyMetadata(null));



        public bool CollideConnected
        {
            get { return (bool)GetValue(CollideConnectedProperty); }
            set { SetValue(CollideConnectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollideConnected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollideConnectedProperty =
            DependencyProperty.Register("CollideConnected", typeof(bool), typeof(RopeJointControl), new PropertyMetadata(true));

        
    }
}
