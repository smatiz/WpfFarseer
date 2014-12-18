using SM;
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
    public partial class MyFirstFarseerUserControl : SM.Xaml.User
    {
        public MyFirstFarseerUserControl()
        {
            InitializeComponent();
        }

        public transform2d UserTransform
        {
            get { return (transform2d)GetValue(UserTransformProperty); }
            set { SetValue(UserTransformProperty, value); }
        }
        public static readonly DependencyProperty UserTransformProperty =
            DependencyProperty.Register("UserTransform", typeof(transform2d), typeof(MyFirstFarseerUserControl), new PropertyMetadata(transform2d.Null, new PropertyChangedCallback(UserTransformPropertyChanged)));
        private static void UserTransformPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((MyFirstFarseerUserControl)obj).OnUserTransformChanged(); }
        private void OnUserTransformChanged()
        {
        }
    }
}
