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
    public partial class FlagControl : BasicControl
    {


        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(float), typeof(FlagControl), new PropertyMetadata(0f, new PropertyChangedCallback(XPropertyChanged)));
        private static void XPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((FlagControl)obj).OnXChanged(); }
        private void OnXChanged()
        {
            Canvas.SetLeft(_crossControl, X * _context.Zoom);
        }



        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(float), typeof(FlagControl), new PropertyMetadata(0f, new PropertyChangedCallback(YPropertyChanged)));
        private static void YPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((FlagControl)obj).OnYChanged(); }
        private void OnYChanged()
        {
            Canvas.SetTop(_crossControl, Y * _context.Zoom);
        }
        
        

   
        CrossControl _crossControl;
        public  FlagControl()
        {
            _crossControl = new SM.Wpf.CrossControl();
            AddChild(_crossControl);


            _context = new DebugContext() { Zoom = 20 };
        }
    }
}
