using SM;
using SM.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SM.Xaml
{
    [ContentPropertyAttribute("Shapes")]
    public class Body : BasicControl, IBody, IFlaggable
    {
        public Body()
        {
            Shapes = new List<IShape>();
            Flags = new List<IFlag>();
        }

        public List<IFlag> Flags
        {
            get { return (List<IFlag>)GetValue(FlagsProperty); }
            set { SetValue(FlagsProperty, value); }
        }
        public static readonly DependencyProperty FlagsProperty =
            DependencyProperty.Register("Flags", typeof(List<IFlag>), typeof(Body), new PropertyMetadata(null));

        //public float X
        //{
        //    get { return (float)GetValue(XProperty); }
        //    set { SetValue(XProperty, value); }
        //}
        //public static readonly DependencyProperty XProperty =
        //    DependencyProperty.Register("X", typeof(float), typeof(Body), new PropertyMetadata(0f));

        //public float Y
        //{
        //    get { return (float)GetValue(YProperty); }
        //    set { SetValue(YProperty, value); }
        //}
        //public static readonly DependencyProperty YProperty =
        //    DependencyProperty.Register("Y", typeof(float), typeof(Body), new PropertyMetadata(0f));

        //public float Angle
        //{
        //    get { return (float)GetValue(AngleProperty); }
        //    set { SetValue(AngleProperty, value); }
        //}
        //public static readonly DependencyProperty AngleProperty =
        //    DependencyProperty.Register("Angle", typeof(float), typeof(Body), new PropertyMetadata(0f));




        public transform2d Transform
        {
            get { return (transform2d)GetValue(TransformProperty); }
            set { SetValue(TransformProperty, value); }
        }
        public static readonly DependencyProperty TransformProperty =
            DependencyProperty.Register("Transform", typeof(transform2d), typeof(Body), new PropertyMetadata(transform2d.Null));

        



        public virtual BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(Body), new PropertyMetadata(BodyType.Static));

        public List<IShape> Shapes
        {
            get { return (List<IShape>)GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(List<IShape>), typeof(Body), new PropertyMetadata(null));
    }
}
