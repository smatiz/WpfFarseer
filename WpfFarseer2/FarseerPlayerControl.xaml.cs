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
    public partial class FarseerPlayerControl : UserControl
    {
        public FarseerPlayerControl()
        {
            InitializeComponent();
        }
        public FarseerCanvas FarseerCanvas
        {
            get { return (FarseerCanvas)GetValue(FarseerCanvasProperty); }
            set { SetValue(FarseerCanvasProperty, value); }
        }
        public static readonly DependencyProperty FarseerCanvasProperty =
            DependencyProperty.Register("FarseerCanvas", typeof(FarseerCanvas), typeof(FarseerPlayerControl), new PropertyMetadata(null, onFarseerCanvasChanged));
        private static void onFarseerCanvasChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var _this = (FarseerPlayerControl)dependencyObject;
            _this.stepControl.DataContext = _this.FarseerCanvas.StepViewModel;

            _this.farseerContainer.Children.Clear();
            _this.farseerContainer.Children.Add(_this.FarseerCanvas);

        }


    }
   
}
