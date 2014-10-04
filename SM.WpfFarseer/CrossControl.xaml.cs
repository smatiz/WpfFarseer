using SM.Farseer;
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
    public partial class CrossControl : BasicControl, IPointObject
    {
        public CrossControl()
        {
            InitializeComponent();


        }

        /*public string TargetName
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
        public static readonly DependencyProperty TargetPointProperty =
            DependencyProperty.Register("TargetPoint", typeof(Point), typeof(CrossControl), new PropertyMetadata(new Point()));
         * */

        public float X { get { return (float)Canvas.GetLeft(this); } }

        public float Y { get { return (float)Canvas.GetTop(this); } }

        public string ParentId { get { return ((BasicControl)Parent).Id; } }
      
    }
}
