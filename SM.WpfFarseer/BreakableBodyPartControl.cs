using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace WpfFarseer
{
    [ContentPropertyAttribute("Shape")]
    public class BreakableBodyPartControl : BodyControl
    {
        public override BodyType BodyType
        {
            get
            {
                return BodyType.Dynamic;
            }
        }

        public Shape Shape
        {
            get { return (Shape)GetValue(ShapeProperty); }
            set { SetValue(ShapeProperty, value); }
        }
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.Register("Shape", typeof(Shape), typeof(BreakableBodyPartControl),
            new PropertyMetadata(null, new PropertyChangedCallback(ShapePropertyChanged)));
        private static void ShapePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var _obj = ((BreakableBodyPartControl)obj);
            _obj.Children.Clear();
            _obj.Children.Add(_obj.Shape);
        }
    }
}
