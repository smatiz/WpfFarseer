using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace SM.Wpf
{
    [ContentPropertyAttribute("Shape")]
    public class AutoBreakableBodyControl : BodyControl
    {
        public virtual TriangulationAlgorithm TriangulationAlgorithm
        {
            get { return (TriangulationAlgorithm)GetValue(TriangulationAlgorithmProperty); }
            set { SetValue(TriangulationAlgorithmProperty, value); }
        }
        public static readonly DependencyProperty TriangulationAlgorithmProperty =
            DependencyProperty.Register("TriangulationAlgorithm", typeof(TriangulationAlgorithm), 
            typeof(AutoBreakableBodyControl), new PropertyMetadata(TriangulationAlgorithm.Bayazit));

        public ShapeControl Shape
        {
            get { return (ShapeControl)GetValue(ShapeProperty); }
            set { SetValue(ShapeProperty, value); }
        }
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.Register("Shape", typeof(ShapeControl), typeof(AutoBreakableBodyControl),
            new PropertyMetadata(null, new PropertyChangedCallback(ShapePropertyChanged)));
        private static void ShapePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //var _obj = ((AutoBreakableBodyControl)obj);
            //_obj.Children.Clear();
            //_obj.Children.Add(_obj.Shape);
        }

      
    }
}
