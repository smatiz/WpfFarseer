using System;
using SM.Farseer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SM.Wpf;
using SM;

namespace WpfFarseer
{
    public class BasicJointControl : BasicControl, IJointControl
    {
        public bool CollideConnected
        {
            get { return (bool)GetValue(CollideConnectedProperty); }
            set { SetValue(CollideConnectedProperty, value); }
        }
        public static readonly DependencyProperty CollideConnectedProperty =
            DependencyProperty.Register("CollideConnected", typeof(bool), typeof(BasicJointControl), new PropertyMetadata(true));
    }
}
