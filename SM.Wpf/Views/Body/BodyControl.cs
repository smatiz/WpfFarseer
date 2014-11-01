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
    //enum xxx
    //{
    //    Convex, Concave, Triangulated
    //}



    public class BasicShapeControl : DependencyObject
    {
        public float Density
        {
            get { return (float)GetValue(DensityProperty); }
            set { SetValue(DensityProperty, value); }
        }
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.Register("Density", typeof(float), typeof(BasicShapeControl), new PropertyMetadata(1f));

    }

    public class PolygonShapeControl : BasicShapeControl, IPolygonShape
    {
        public PolygonShapeControl()
        {

        }

        public PolygonShapeControl(IEnumerable<float2> poly, float density)
        {
            //Points = poly;
            Density = density;
        }

        public PointCollection Points
        {
            get { return (PointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(PointCollection), typeof(PolygonShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(Points_PropertyChanged)));
        static void Points_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public IEnumerable<float2> Points_X { get; private set; }
        public float Density { get; private set; }

    }

    [ContentPropertyAttribute("Content")]
    public class SkinnedShapeControl : BasicShapeControl, IPolygonsShape
    {
        public static __ISkinnableCanvas Skinner { private get; set; }
        public IEnumerable<IEnumerable<float2>> PolygonShapes
        {
            get
            {
                return Skinner.__FindBorder(_brush, MaxWidth, MaxHeight);
            }
        }

        
        public int MaxWidth
        {
            get { return (int)GetValue(MaxWidthProperty); }
            set { SetValue(MaxWidthProperty, value); }
        }
        public static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.Register("MaxWidth", typeof(int), typeof(SkinnedShapeControl), new PropertyMetadata(1000));

        public int MaxHeight
        {
            get { return (int)GetValue(MaxHeightProperty); }
            set { SetValue(MaxHeightProperty, value); }
        }
        public static readonly DependencyProperty MaxHeightProperty =
            DependencyProperty.Register("MaxHeight", typeof(int), typeof(SkinnedShapeControl), new PropertyMetadata(1000));

        VisualBrush _brush;
        public Canvas Content
        {
            get { return (Canvas)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(Canvas), typeof(SkinnedShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(ContentPropertyChanged)));
        private static void ContentPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((SkinnedShapeControl)obj).OnContentChanged(); }
        private void OnContentChanged()
        {
            //Content.Loaded += Content_Loaded;
            Content.Measure(new Size(MaxWidth, MaxHeight));
            Content.Arrange(new Rect(0, 0, MaxWidth, MaxHeight));
            Content.UpdateLayout();
            _brush = Content.GetVisualBrush();
            Content.UpdateLayout();
        }
        
       
    }


    [ContentPropertyAttribute("Shapes")]
    public class __BodyControl : BasicControl, IFlaggable, __IBodyView
    {
        private RotateTransform _rotation;
        private TranslateTransform _traslation;

        public __BodyControl()
        {
            initializeShapes(); 
            initializeFlags();

            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            bool loaded = false;
            _canvas.Loaded += (s, e) =>
            {
                if (loaded) return;
                loaded = true;
                var gt = new TransformGroup();
                gt.Children.Add(_rotation);
                gt.Children.Add(_traslation);
                _canvas.RenderTransform = gt;


                OnLoaded();
            };
        }

        protected virtual void OnLoaded()
        {
            foreach (var shape in Shapes)
            {
                var skinned = shape as SkinnedShapeControl;
                if (skinned != null)
                {
                    _canvas.Children.Add(skinned.Content);
                }
            }
        }

        public ObservableCollection<FlagControl> Flags
        {
            get { return (ObservableCollection<FlagControl>)GetValue(FlagsProperty); }
            set { SetValue(FlagsProperty, value); }
        }
        public static readonly DependencyProperty FlagsProperty =
            DependencyProperty.Register("Flags", typeof(ObservableCollection<FlagControl>), typeof(__BodyControl), new PropertyMetadata(null));
        void Flags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    ((FlagControl)x).AddToUIElementCollection(_canvas.Children);
                    //_canvas.Children.Add((FlagControl)x);
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
            DependencyProperty.Register("X", typeof(float), typeof(__BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(XPropertyChanged)));
        private static void XPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((__BodyControl)obj).OnXChanged(); }
        private void OnXChanged()
        {
            _traslation.X = X;
        }

        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(float), typeof(__BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(YPropertyChanged)));
        private static void YPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((__BodyControl)obj).OnYChanged(); }
        private void OnYChanged()
        {
            _traslation.Y = Y;
        }

        public float Angle
        {
            get { return (float)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(float), typeof(__BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(AnglePropertyChanged)));
        private static void AnglePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((__BodyControl)obj).OnAngleChanged(); }
        private void OnAngleChanged()
        {
            _rotation.Angle = Angle;
        }

        public virtual __BodyType BodyType
        {
            get { return (__BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(__BodyType), typeof(__BodyControl), new PropertyMetadata(__BodyType.Static));

        public ObservableCollection<__IShape> Shapes
        {
            get { return (ObservableCollection<__IShape>)GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(ObservableCollection<__IShape>), typeof(__BodyControl), new PropertyMetadata(null));

        void Shapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {

                }
            }
        }
        private void initializeShapes()
        {
            Shapes = new ObservableCollection<__IShape>();
            Shapes.CollectionChanged += Shapes_CollectionChanged;
        }

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

        public IEnumerable<__IShape> Shapes_X
        {
            get { return from x in Shapes select x; }
        }

        public IEnumerable<__IBodyView> Break()
        {
            _canvas.Children.Clear();
            foreach (var shape in Shapes)
            {

                if (shape is ICircleShape || shape is IPolygonShape)
                {
                    var bc = new __BodyControl();
                    bc.BodyType = SM.__BodyType.Dynamic;
                    bc.Shapes.Add(shape);
                    bc.AddToUIElementCollection(_parentChildrens);
                    bc.RotoTranslation = RotoTranslation;
                    yield return bc;
                }
                else if (shape is IPolygonsShape)
                {
                    foreach (var subshape in ((IPolygonsShape)shape).PolygonShapes)
                    {
                        var bc = new __BodyControl();
                        bc.BodyType = SM.__BodyType.Dynamic;
                        bc.Shapes.Add(new PolygonShapeControl(subshape, ((IPolygonsShape)shape).Density));
                        bc.AddToUIElementCollection(_parentChildrens);
                        bc.RotoTranslation = RotoTranslation;
                        yield return bc;
                    }
                }


                

            }
        }


        //private IEnumerable<__IBodyView> ToBodyViews(__IShape shape)
        //{
        //    if (shape is ICircleShape || shape is IPolygonShape)
        //    {
        //        var bc = new __BodyControl();
        //        bc.Shapes.Add(shape);
        //        yield return bc;
        //    }


        //    var polys = shape as IPolygonsShape;
        //    if (polys != null)
        //    {
        //        foreach (var x in polys.PolygonShapes)
        //        {
        //            var bc = new __BodyControl();
        //            bc.Shapes.Add(new PolygonShapeControl(x, polys.Density));
        //            yield return bc;
        //        }
        //    }
        //}



    }


    [ContentPropertyAttribute("Shapes")]
    public class BodyControl : BasicBodyControl, IBodyView
    {

        public BodyControl()
        {
            Shapes = new ObservableCollection<ShapeControl>();
            Shapes.CollectionChanged += Shapes_CollectionChanged;
        }
        
        protected override Brush Brush { get { return DefaultBrush; } }
        public Brush DefaultBrush
        {
            private get { return (Brush)GetValue(DefaultBrushProperty); }
            set { SetValue(DefaultBrushProperty, value); }
        }
        public static readonly DependencyProperty DefaultBrushProperty =
            DependencyProperty.Register("DefaultBrush", typeof(Brush), typeof(BodyControl), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public virtual BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(BodyControl), new PropertyMetadata(BodyType.Static));

        public ObservableCollection<ShapeControl> Shapes
        {
            get { return (ObservableCollection<ShapeControl>)GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(ObservableCollection<ShapeControl>), typeof(BodyControl), new PropertyMetadata(null));

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
        
        public IEnumerable<IShape> Shapes_X
        {
            get { return from x in Shapes select x; }
        }

        protected override IEnumerable<Polygon> Polygons
        {
            get
            {
                return from x in Shapes select (Polygon)x.Shape;
            }
        }
    }
}
