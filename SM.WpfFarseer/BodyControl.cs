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
            Shapes = new ObservableCollection<ShapeControl>();
            Shapes.CollectionChanged += Shapes_CollectionChanged;

            Flags = new ObservableCollection<FlagControl>();
            Flags.CollectionChanged += Flags_CollectionChanged;

            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            _canvas.Loaded += (s, e) =>
            {
                var gt = new TransformGroup();
                gt.Children.Add(_rotation);
                gt.Children.Add(_traslation);
                _canvas.RenderTransform = gt;
                Update();
            };
        }

        public void Update()
        {
            var brush = DefaultBrush;

            foreach (var shape in Shapes)
            {
                var poly = shape.Shape;
                _canvas.Children.Add(poly);
                ((Polygon)poly).Fill = brush;
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

        public virtual BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(BodyControl), new PropertyMetadata(BodyType.Static));

        public void Set(float x, float y, float a)
        {
            _traslation.X = x;
            _traslation.Y = y;
            _rotation.Angle = a * AngleSubst;
        }

        //private FarseerPhysics.Dynamics.Fixture _getAttachFixture(ShapeControl shape, FarseerPhysics.Dynamics.Body body)
        //{
        //    return FarseerPhysics.Factories.FixtureFactory.AttachPolygon(shape.Points.ToFarseerVertices(), BodyControl.GetDensity(shape), body);
        //} 

        //public IEnumerable<FarseerPhysics.Dynamics.Fixture> GetAttachFixtures(FarseerPhysics.Dynamics.Body body)
        //{
        //    return from shape in Shapes select _getAttachFixture(shape, body);
        //}

        public ObservableCollection<ShapeControl> Shapes
        {
            get { return (ObservableCollection<ShapeControl>)GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(ObservableCollection<ShapeControl>), typeof(BodyControl), new PropertyMetadata(null));

        public ObservableCollection<FlagControl> Flags
        {
            get { return (ObservableCollection<FlagControl>)GetValue(FlagsProperty); }
            set { SetValue(FlagsProperty, value); }
        }
        public static readonly DependencyProperty FlagsProperty =
            DependencyProperty.Register("Flags", typeof(ObservableCollection<FlagControl>), typeof(BodyControl), new PropertyMetadata(null));

        void Shapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    _canvas.Children.Add(((ShapeControl)x).Shape);
                }
            }
        }
        
        void Flags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    _canvas.Children.Add((FlagControl)x);
                }
            }
        }
        
        public IEnumerable<IPointControl> FlagsPoints
        {
            get { return from x in Flags select x; }
        }

        public IEnumerable<IShape> Shapes_X
        {
            get { return from x in Shapes select x; }
        }



    }
}
