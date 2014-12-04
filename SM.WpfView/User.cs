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
using SM.WpfView;

namespace SM.Xaml
{
    [ContentPropertyAttribute("Children")]
    public class User : UserControl, ILayer, IDescriptor
    {
        public User()
        {
            Children = new List<IDescriptor>();

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                Loaded += User_Loaded;
            }
        }



        Info _farseerInfo;
        Views _farseerViews;
        Context _context;
        RootView _root;
        void User_Refresh_DesignTime()
        {
            _context = new Context(DesignZoom);
            var c = new Canvas();
            Content = c;
            _root = new RootView(_context, c);
            SM.WpfView.Helper.LoadFarseer(Id, Children, _root, out _farseerInfo, out _farseerViews);
        }
        void User_Loaded(object sender, RoutedEventArgs e)
        {
            User_Refresh_DesignTime();
        }




        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(string), typeof(User), new PropertyMetadata(null));



        public float DesignZoom
        {
            get { return (float)GetValue(DesignZoomProperty); }
            set { SetValue(DesignZoomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesignZoom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesignZoomProperty =
            DependencyProperty.Register("DesignZoom", typeof(float), typeof(User), new PropertyMetadata(1f));




        public List<IDescriptor> Children
        {
            get { return (List<IDescriptor>)GetValue(FarseerObjectsProperty); }
            set { SetValue(FarseerObjectsProperty, value); }
        }
        public static readonly DependencyProperty FarseerObjectsProperty =
            DependencyProperty.Register("Children", typeof(List<IDescriptor>), typeof(User), new PropertyMetadata(null));




        public transform2d Transform
        {
            get { return (transform2d)GetValue(TransformProperty); }
            set { SetValue(TransformProperty, value); }
        }
        public static readonly DependencyProperty TransformProperty =
            DependencyProperty.Register("Transform", typeof(transform2d), typeof(User),
            new FrameworkPropertyMetadata(transform2d.Null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(TransformPropertyChanged)));
        private static void TransformPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((User)obj).OnTransformChanged(); }
        private void OnTransformChanged()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                User_Refresh_DesignTime();
            }
        }
        

        //public transform2d Transform
        //{
        //    get { return (transform2d)GetValue(TransformProperty); }
        //    set { SetValue(TransformProperty, value); }
        //}
        //public static readonly DependencyProperty TransformProperty =
        //    DependencyProperty.Register("Transform", typeof(transform2d), typeof(User), new PropertyMetadata(transform2d.Null));


        //public IEnumerable<IDescriptor> Descriptors { get { return Children.Select(c => (IDescriptor)c); } }



    }
}
