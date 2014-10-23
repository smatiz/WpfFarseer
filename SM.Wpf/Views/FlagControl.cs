using SM;
using SM.Wpf;
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

namespace SM.Wpf
{
    public partial class FlagControl : BasicControl, IPointControl
    {

        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(float), typeof(FlagControl), new PropertyMetadata(0f));

        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(float), typeof(FlagControl), new PropertyMetadata(0f));

        //Visual _root;
        //public void Set(Visual root)
        //{
        //    _root = root;
        //}


        public string ParentId { get; set; }// ((BasicControl)_parent).Id; } }
        public string Id { get; set; }


        //public Point AbsoluteLocation
        //{
        //    get
        //    {
        //        return TransformToAncestor(_root).Transform(new Point(X, Y)); ;
        //    }
        //}

        

    }
}
