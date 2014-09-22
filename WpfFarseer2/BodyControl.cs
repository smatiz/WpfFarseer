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
    public class BodyControl : Canvas
    {
        public RotateTransform Rotation { get; private set; }
        public TranslateTransform Traslation { get; private set; }
        
        public BodyControl()
        {
            Rotation = new RotateTransform();
            Traslation = new TranslateTransform();

            Loaded += (s, e) =>
            {
                var rt = this.RenderTransform;
                var gt = new TransformGroup();
                gt.Children.Add(Rotation);
                gt.Children.Add(Traslation);
                gt.Children.Add(rt);
                this.RenderTransform = gt;
                _refreshVisual();
            };
        }

        public void Update()
        {
            _refreshVisual();
        }
        public IEnumerable<Shape> FindShapes()
        {
            foreach (var x in Children)
            {
                if (x is Shape)
                {
                    yield return x as Shape;
                }
            }
        }

        private Brush _getBrush()
        {
            switch (BodyType)
            {
                case FarseerPhysics.Dynamics.BodyType.Static:
                    return new SolidColorBrush(Colors.Black);
                case FarseerPhysics.Dynamics.BodyType.Kinematic:
                    return new SolidColorBrush(Colors.Blue);
                case FarseerPhysics.Dynamics.BodyType.Dynamic:
                    return new SolidColorBrush(Colors.Orange);
                default:
                    return null;
            }
        }
        private void _refreshVisual()
        {
            var brush =  _getBrush();

            foreach (var shape in FindShapes())
            {
                shape.Fill = brush;
            }

        }

        public static float GetDensity(DependencyObject obj)
        {
            return (float)obj.GetValue(DensityProperty);
        }
        public static void SetDensity(DependencyObject obj, float value)
        {
            obj.SetValue(DensityProperty, value);
        }
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.RegisterAttached("Density", typeof(float), typeof(BodyControl), new PropertyMetadata(Const.Density));

        public FarseerPhysics.Dynamics.BodyType BodyType
        {
            get { return (FarseerPhysics.Dynamics.BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(FarseerPhysics.Dynamics.BodyType), typeof(BodyControl), new PropertyMetadata(FarseerPhysics.Dynamics.BodyType.Static));
    }
}
