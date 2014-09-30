using FarseerPhysics.Dynamics;
using SM.Farseer;
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

    public class BreakableBodyContol : BasicControl, IBreakableBodyObject
    {
        const float AngleSubst = 180f / (float)Math.PI;

        private RotateTransform _rotation; //{ get; private set; }
        private TranslateTransform _traslation;// { get; private set; }

        private List<PartControl> _partControls = new List<PartControl>();

        public BreakableBodyContol()
        {
            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            Loaded += (s, e) =>
            {
                var rt = this.RenderTransform;
                var gt = new TransformGroup();
                gt.Children.Add(_rotation);
                gt.Children.Add(_traslation);
                gt.Children.Add(rt);
                this.RenderTransform = gt;


                foreach (var x in Children.OfType<PartControl>())
                {
                    //_partControls.Add(new PartControl)
                }


                _refreshVisual();
            };
        }

        void _break()
        {
            foreach (var x in Children.OfType<PartControl>())
            {
                
            }
        }

        public void Update()
        {
            _refreshVisual();
        }
        

        private void _refreshVisual()
        {
            var brush = DefaultBrush;

            foreach (var shape in Children.OfType<Shape>())
            {
                shape.Fill = brush;
            }

        }



        public Brush DefaultBrush
        {
            private get { return (Brush)GetValue(DefaultBrushProperty); }
            set { SetValue(DefaultBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultBrushProperty =
            DependencyProperty.Register("DefaultBrush", typeof(Brush), typeof(BodyControl), new PropertyMetadata(null));



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


        public void Set(float x, float y, float a)
        {
            _traslation.X = x;
            _traslation.Y = y;
            _rotation.Angle = a * AngleSubst;
        }

        public IBodyObject Get(Fixture fixture)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FarseerPhysics.Dynamics.Fixture> GetAttachFixtures(Body body)
        {
            return from shape in Children.OfType<Shape>() select this.ToFarseer(shape, body);
        }

    }
}
