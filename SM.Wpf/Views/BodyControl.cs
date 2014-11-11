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

namespace SM.Wpf
{
    [ContentPropertyAttribute("Shapes")]
    public class BodyControl : BasicControl, IFlaggable, IBodyView
    {
        class SkinnedShapeItem
        {
            public Polygon Polygon { get; set; }
            public SkinnedShapeControl ShapeControl { get; set; }
        }

        private Canvas _canvas;
        private VisualBrush _visualBrush;
        private RotateTransform _rotation;
        private TranslateTransform _traslation;


        public BodyControl()
        {
            Shapes = new List<IShape>();
            initializeFlags();

            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            Context = new DefaultContext();
        }

        public override IContext Context
        {
            set
            {
                base.Context = value;
                if (Shapes != null)
                {
                    foreach (var s in Shapes)
                    {
                        s.Context = value;
                        _traslation.X = X * _context.Zoom;
                        _traslation.Y = Y * _context.Zoom;
                    }
                }
            }
        }
        
        //transform2d _transform2d;
        //public transform2d Transform2d
        //{
        //    set
        //    {
        //        _transform2d = value;
        //    }
        //}

       protected override void OnFirstLoad()
        {
           _canvas = new Canvas();
           AddChild(_canvas);
            var gt = new TransformGroup();
            gt.Children.Add(_rotation);
            gt.Children.Add(_traslation);
            _canvas.RenderTransform = gt;

            foreach (var shape in Shapes)
            {
                shape.Context = _context;
                var drawable = shape as IDrawable;
                if (drawable != null)
                {
                    _canvas.Children.Add(drawable.UIElement);
                }
            }
        }

        public ObservableCollection<FlagControl> Flags
        {
            get { return (ObservableCollection<FlagControl>)GetValue(FlagsProperty); }
            set { SetValue(FlagsProperty, value); }
        }
        public static readonly DependencyProperty FlagsProperty =
            DependencyProperty.Register("Flags", typeof(ObservableCollection<FlagControl>), typeof(BodyControl), new PropertyMetadata(null));
        void Flags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    AddChild((FlagControl)x);
                }
            }
        }
        private void initializeFlags()
        {
            Flags = new ObservableCollection<FlagControl>();
            Flags.CollectionChanged += Flags_CollectionChanged;
        }

        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(float), typeof(BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(XPropertyChanged)));
        private static void XPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((BodyControl)obj).OnXChanged(); }
        private void OnXChanged()
        {
            _traslation.X = X * _context.Zoom;
        }

        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(float), typeof(BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(YPropertyChanged)));
        private static void YPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((BodyControl)obj).OnYChanged(); }
        private void OnYChanged()
        {
            _traslation.Y = Y * _context.Zoom;
        }

        public float Angle
        {
            get { return (float)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(float), typeof(BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(AnglePropertyChanged)));
        private static void AnglePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((BodyControl)obj).OnAngleChanged(); }
        private void OnAngleChanged()
        {
            _rotation.Angle = Angle;
        }

        public virtual BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(BodyControl), new PropertyMetadata(BodyType.Static));

        public List<IShape> Shapes
        {
            get { return (List<IShape>)GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(List<IShape>), typeof(BodyControl), new PropertyMetadata(null));


        public rotoTranslation RotoTranslation
        {
            get
            {
                return rotoTranslation.FromDegree(new float2((float)_traslation.X, (float)_traslation.Y), (float)_rotation.Angle);
            }
            set
            {
                X = value.Translation.X;
                Y = value.Translation.Y;
                Angle = value.DegreeAngle;
            }
        }

        public IEnumerable<IBodyView> Break()
        {
            var breakableShapes = Shapes.Where(x => x is IBreakableShape).Select<IShape, IBreakableShape>(x => (IBreakableShape)x);
            Rect maxbb = Rect.Empty;
            foreach (var breakableShape in breakableShapes)
            {
                maxbb.Union(breakableShape.BBox);
            }
            List<BodyControl> bodies = new List<BodyControl>();
            _canvas.Update();
            //_canvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //_canvas.Arrange(new Rect(new Point(), _canvas.DesiredSize));
            //_canvas.UpdateLayout();
            _rotation.Angle = 0;
            _traslation.X = 0;
            _traslation.Y = 0;
            _visualBrush = new VisualBrush(_canvas);
            foreach (var breakableShape in breakableShapes)
            {
                foreach (var p in breakableShape.PointCollections)
                {
                    var polyShape = new ConvexPolygonShapeControl();
                    polyShape.Density = breakableShape.Density;
                    var vbClone = _visualBrush.Clone();
                    polyShape.PointCollection = p.Clone();
                    var polygonBB = polyShape.BBox;
                    vbClone.Viewbox = new Rect((polygonBB.X - maxbb.X) / maxbb.Width, (polygonBB.Y - maxbb.Y) / maxbb.Height, polygonBB.Width / maxbb.Width, polygonBB.Height / maxbb.Height);

                    polyShape.Fill = vbClone;

                    var bc = new BodyControl();
                    bc.Context = _context;
                    bc.BodyType = SM.BodyType.Dynamic;
                    bc.Shapes.Add(polyShape);
                    AddChild(bc);
                    bc.RotoTranslation = RotoTranslation;
                    bodies.Add(bc);
                }
            }
            RemoveChild(_canvas);
            return bodies;
        }
    }
}
