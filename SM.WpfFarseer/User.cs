using SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using SM.Wpf;

namespace SM.Xaml
{
    public class DesignerCanvas : Canvas, IDescriptor
    {

    }


    public class User : Layer, IIdentifiable
    {
        string _id;
        public string Id 
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
        }

        public float DesignZoom
        {
            get { return (float)GetValue(DesignZoomProperty); }
            set { SetValue(DesignZoomProperty, value); }
        }
        public static readonly DependencyProperty DesignZoomProperty =
            DependencyProperty.Register("DesignZoom", typeof(float), typeof(User), new PropertyMetadata(1f));

        public User()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                Loaded += User_Loaded;
            }
        }

        Info _farseerInfo;
        Views _farseerViews;
        bool oneTimeCalled;
        void User_Loaded(object sender, RoutedEventArgs e)
        {
            if (oneTimeCalled) return;
            oneTimeCalled = true;
            var _context = new Context(DesignZoom);

            var canvas = new DesignerCanvas();
            canvas.Width = 1000;
            canvas.Height = 1000;
            //Children.Add(canvas);


            //canvas.Children.Add(this); 
            //_farseerCanvas.Children.Add(_farseer); 
            //SM.WpfView.Helper.LoadFarseer(Farseer, _farseerCanvas, _context, out _farseerInfo, out _farseerViews);

            SM.WpfView.Helper.LoadFarseer(this, canvas, _context, out _farseerInfo, out _farseerViews);
            //MessageBox.Show("aaaaaaaa");
        }
    }
}
