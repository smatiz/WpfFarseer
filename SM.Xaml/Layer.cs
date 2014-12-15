using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SM.Xaml
{
    public class Layer : BasicContainer, ILayer
    {
        public transform2d Transform
        {
            get { return (transform2d)GetValue(TransformProperty); }
            set { SetValue(TransformProperty, value); }
        }
        public static readonly DependencyProperty TransformProperty =
            DependencyProperty.Register("Transform", typeof(transform2d), typeof(Layer), new FrameworkPropertyMetadata(transform2d.Null, FrameworkPropertyMetadataOptions.AffectsRender));



        //public transform2d Transform
        //{
        //    get { return (transform2d)GetValue(TransformProperty); }
        //    set { SetValue(TransformProperty, value); }
        //}
        //public static readonly DependencyProperty TransformProperty =
        //    DependencyProperty.Register("Transform", typeof(transform2d), typeof(User),
        //    new FrameworkPropertyMetadata(transform2d.Null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(TransformPropertyChanged)));
        //private static void TransformPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((User)obj).OnTransformChanged(); }
        //private void OnTransformChanged()
        //{
        //    if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        //    {
        //        //User_Refresh_DesignTime();
        //    }
        //}
       
    }
}
