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


        private VisualBrush _visualBrush;
        private RotateTransform _rotation;
        private TranslateTransform _traslation;

        public BodyControl()
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
            DependencyProperty.Register("X", typeof(float), typeof(BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(XPropertyChanged)));
        private static void XPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((BodyControl)obj).OnXChanged(); }
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
            DependencyProperty.Register("Y", typeof(float), typeof(BodyControl), new PropertyMetadata(0f, new PropertyChangedCallback(YPropertyChanged)));
        private static void YPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((BodyControl)obj).OnYChanged(); }
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

        public ObservableCollection<IShape> Shapes
        {
            get { return (ObservableCollection<IShape>)GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(ObservableCollection<IShape>), typeof(BodyControl), new PropertyMetadata(null));

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
            Shapes = new ObservableCollection<IShape>();
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

        public IEnumerable<IShape> AllShapes
        {
            get { return from x in Shapes select x; }
        }

        private void OnLoaded()
        {
            foreach (var shape in Shapes)
            {
                var drawable = shape as IDrawable;
                if (drawable != null)
                {
                    _canvas.Children.Add(drawable.UIElement);
                }
            }

            //if (_canvas.Id == "cippo")
            //{
            //    Helper.FarseerTools.Save(_visualBrush, @"C:\Users\Developer\Desktop\temp\aaaa1.png");
            //    var t = _rotation.Angle;
            //    _rotation.Angle = 0;
            //    Helper.FarseerTools.Save(_visualBrush, @"C:\Users\Developer\Desktop\temp\aaaa2.png");
            //    _rotation.Angle = t;
            //    Helper.FarseerTools.Save(_visualBrush, @"C:\Users\Developer\Desktop\temp\aaaa3.png");
            //}
        }


        //private VisualBrush CloneVisualBrush()
        //{
        //    VisualBrush vb;
        //    var t = _rotation.Angle;
        //    _rotation.Angle = 0;
        //    vb = _visualBrush.Clone();
        //    _rotation.Angle = t;
        //    return vb;
        //}

        public IEnumerable<IBodyView> Break()
        {
            //if (_canvas.Id == "cippo")
            //    Helper.FarseerTools.Save(_visualBrush, @"C:\Users\Developer\Desktop\temp\bbb.png");
            var boxeds = Shapes.Where(x => x is IBreakableShape).Select<IShape, IBreakableShape>(x => (IBreakableShape)x);

            Rect maxbb = Rect.Empty;
            foreach (var boxed in boxeds)
            {
                maxbb.Union(boxed.BBox);
            }
            List<BodyControl> bodies = new List<BodyControl>();


            _canvas.Measure(new Size(maxbb.Width, maxbb.Height));
            _canvas.Arrange(new Rect(new Point(), _canvas.DesiredSize));
            _canvas.UpdateLayout();
            _visualBrush = new VisualBrush(_canvas);
            
            //_visualBrush.Freeze();
            _rotation.Angle = 0;
            _traslation.X = 0;
            _traslation.Y = 0;

            int i = 0;
            foreach (var boxed in boxeds)
            {
                var vbClone = _visualBrush.Clone();

                foreach (var p in boxed.Polygons)
                {
                    var pclone = new Polygon();
                    pclone.Points = p.Points.Clone();
                    //pclone.Stroke = new SolidColorBrush(Colors.Blue);
                    var polygonBB = pclone.BBox();
                    //vb.Viewbox = new Rect(-(bb.X - bbs[i].X) / bb.Width, -(bb.Y - bbs[i].Y) / bb.Height, bbs[i].Width / bb.Width, bbs[i].Height / bb.Height);

                    Helper.FarseerTools.Save(vbClone, @"C:\Users\Developer\Desktop\temp\aaa" + (i).ToString() + ".png");
                    vbClone.Viewbox = new Rect((polygonBB.X - maxbb.X) / maxbb.Width, (polygonBB.Y - maxbb.Y) / maxbb.Height, polygonBB.Width / maxbb.Width, polygonBB.Height / maxbb.Height);
                    Helper.FarseerTools.Save(vbClone, @"C:\Users\Developer\Desktop\temp\bbb" + (i).ToString() + ".png");

                    if (vbClone.Viewbox.X == 0)
                    {
                        pclone.Fill = vbClone;
                        Helper.FarseerTools.Save(vbClone, @"C:\Users\Developer\Desktop\temp\xxx" + (i).ToString() + ".png");

                    }
                    i++;
                    //vbClone.Viewbox = new Rect((polygonBB.X), (polygonBB.Y), polygonBB.Width, polygonBB.Height);

                    //vbClone.Viewport = new Rect((maxbb.X), (maxbb.Y), maxbb.Width, maxbb.Height);


                    //vbClone.AlignmentX = AlignmentX.Left;
                    //vbClone.AlignmentY = AlignmentY.Top;
                    //vbClone.Stretch = Stretch.None;
                    //vbClone.TileMode = TileMode.None;

                    var bc = new BodyControl();
                    bc.BodyType = SM.BodyType.Dynamic;
                    bc.Shapes.Add(new ConvexPolygonShapeControl(pclone, boxed.Density));
                    AddChild(bc);
                    //bc.AddToUIElementCollection(_parentChildrens);
                    bc.RotoTranslation = RotoTranslation;
                    bodies.Add(bc);
                }
            }

            Clear();
            return bodies;
        }





    }
}
