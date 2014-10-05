using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SM.Farseer;
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

namespace WpfFarseer
{

    [ContentPropertyAttribute("Shapes")]
    public class BodyControl : BasicControl, IBodyControl
    {
        const float AngleSubst = 180f / (float)Math.PI;

        private RotateTransform _rotation; 
        private TranslateTransform _traslation;

        public BodyControl()
        {
            Shapes = new ObservableCollection<Shape>();
            Shapes.CollectionChanged += Shapes_CollectionChanged;

            Flags = new ObservableCollection<CrossControl>();
            Flags.CollectionChanged += Flags_CollectionChanged;

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
                _refreshVisual();
            };
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
        public static readonly DependencyProperty DefaultBrushProperty =
            DependencyProperty.Register("DefaultBrush", typeof(Brush), typeof(BodyControl), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

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

        public virtual FarseerPhysics.Dynamics.BodyType BodyType
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


        private Fixture _getAttachFixture(Shape shape, Body body)
        {
            if (shape is Polygon)
            {
                return FixtureFactory.AttachPolygon(this.ToFarseerVertices((Polygon)shape), BodyControl.GetDensity(shape), body);
            }
            //else if (shape is System.Windows.Shapes.Path)
            //{
            //    return FixtureFactory.AttachPolygon(uielement.ToFarseer((Polygon)shape), BodyControl.GetDensity(shape), body);
            //}
            else
            {
                return FixtureFactory.AttachCircle(1, BodyControl.GetDensity(shape), body);
            }
        } 

        public IEnumerable<FarseerPhysics.Dynamics.Fixture> GetAttachFixtures(Body body)
        {
            return from shape in Shapes select _getAttachFixture(shape, body);
        }

       
        //public IEnumerable<Shape> Shapes
        //{
        //    get
        //    {
                
        //        return Children.OfType<Shape>();
        //    }
        //}




        public ObservableCollection<Shape>  Shapes
        {
            get { return (ObservableCollection<Shape> )GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Shapes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(ObservableCollection<Shape> ), typeof(BodyControl), new PropertyMetadata(null));


        public ObservableCollection<CrossControl> Flags
        {
            get { return (ObservableCollection<CrossControl>)GetValue(FlagsProperty); }
            set { SetValue(FlagsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Flags.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlagsProperty =
            DependencyProperty.Register("Flags", typeof(ObservableCollection<CrossControl>), typeof(BodyControl), new PropertyMetadata(null));

        
        

        void Shapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    Children.Add((Shape)x);
                }
            }
        }
        
        void Flags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    Children.Add((CrossControl)x);
                }
            }
        }
        
        //public ObservableCollection<BreakableBodyPartControl> Parts
        //{
        //    get { return (ObservableCollection<BreakableBodyPartControl>)GetValue(PartsProperty); }
        //    set { SetValue(PartsProperty, value); }
        //}
        //public static readonly DependencyProperty PartsProperty =
        //    DependencyProperty.Register("Parts", typeof(ObservableCollection<BreakableBodyPartControl>), typeof(BreakableBodyControl), new PropertyMetadata(null));
   



        public IEnumerable<IPointControl> Points
        {
            get { return from x in Children.OfType<CrossControl>() select x; }
        }
    }
}
