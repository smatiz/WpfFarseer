using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFarseer
{
    /// <summary>
    /// Interaction logic for CrossControl.xaml
    /// </summary>
    public partial class CrossControl : UserControl
    {
        public CrossControl()
        {
            InitializeComponent();
        }



        public string TargetName
        {
            get { return (string)GetValue(TargetNameProperty); }
            set { SetValue(TargetNameProperty, value); }
        }
        public static readonly DependencyProperty TargetNameProperty =
            DependencyProperty.Register("TargetName", typeof(string), typeof(CrossControl), new PropertyMetadata(null));



        public Point TargetPoint
        {
            get { return (Point)GetValue(TargetPointProperty); }
            set { SetValue(TargetPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetPointProperty =
            DependencyProperty.Register("TargetPoint", typeof(Point), typeof(CrossControl), new PropertyMetadata(new Point()));

        

        
    }
}
