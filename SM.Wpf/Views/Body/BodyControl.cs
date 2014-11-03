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
    public class __BodyControl : BasicControl, IFlaggable, __IBodyView
    {
        class SkinnedShapeItem
        {
            public Polygon Polygon { get; set; }
            public SkinnedShapeControl ShapeControl { get; set; }
        }


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
                var poly = shape as PolygonShapeControl;
                if (poly != null)
                {
                    _canvas.Children.Add(poly.Polygon);
                }
                var ellipse = shape as EllipseShapeControl;
                if (ellipse != null)
                {
                    _canvas.Children.Add(ellipse.Ellipse);
                }
                var circle = shape as CircleShapeControl;
                if (circle != null)
                {
                    _canvas.Children.Add(circle.Ellipse);
                }
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
            var filler = new VisualBrushFiller();
            var skinned = new List<SkinnedShapeItem>();
            var polygons = new List<PolygonShapeControl>();
            List<Ellipse> ellipses = new List<Ellipse>();
            foreach (var shape in Shapes)
            {
                if (shape is PolygonShapeControl)
                {
                    var p = ((PolygonShapeControl)shape).Polygon;
                    filler.Add(p);
                    polygons.Add((PolygonShapeControl)shape);
                }
                else if (shape is CircleShapeControl)
                {
                    filler.Add(((CircleShapeControl)shape).Ellipse);
                }
                else if (shape is EllipseShapeControl)
                {
                    filler.Add(((EllipseShapeControl)shape).Ellipse);
                }
                else if (shape is SkinnedShapeControl)
                {

                    foreach (var subshape in ((SkinnedShapeControl)shape).PolygonShapes)
                    {
                        var p = subshape.ToWpfPolygon();
                        filler.Add(p);
                        skinned.Add(new SkinnedShapeItem { Polygon = p, ShapeControl = (SkinnedShapeControl)shape });
                    }
                }
            }

            _canvas.Children.Clear();
            List<__BodyControl> bodies = new List<__BodyControl>();
            foreach (var poly in polygons)
            {
                var bc = new __BodyControl();
                bc.BodyType = SM.__BodyType.Dynamic;
                bc.Shapes.Add(new PolygonShapeControl(poly.Polygon, poly.Density));
                bc.AddToUIElementCollection(_parentChildrens);
                bc.RotoTranslation = RotoTranslation;
                bodies.Add(bc);
            }



            foreach (var poly in skinned)
            {
                var vb = filler.GetBrush(poly.Polygon, poly.ShapeControl.Content);
                poly.Polygon.Fill = vb;

                var bc = new __BodyControl();
                bc.BodyType = SM.__BodyType.Dynamic;
                bc.Shapes.Add(new PolygonShapeControl(poly.Polygon, poly.ShapeControl.Density));
                bc.AddToUIElementCollection(_parentChildrens);
                bc.RotoTranslation = RotoTranslation;
                bodies.Add(bc);
            }

            return bodies;
            //_canvas.Children.Clear();
            //foreach (var shape in Shapes)
            //{

            //    if (shape is ICircleShape || shape is IPolygonShape)
            //    {
            //        var bc = new __BodyControl();
            //        bc.BodyType = SM.__BodyType.Dynamic;
            //        bc.Shapes.Add(shape);
            //        bc.AddToUIElementCollection(_parentChildrens);
            //        bc.RotoTranslation = RotoTranslation;
            //        yield return bc;
            //    }
            //    else if (shape is IPolygonsShape)
            //    {
            //        foreach (var subshape in ((IPolygonsShape)shape).PolygonShapes)
            //        {
            //            var bc = new __BodyControl();
            //            bc.BodyType = SM.__BodyType.Dynamic;
            //            //bc.Shapes.Add(new PolygonShapeControl(subshape, ((IPolygonsShape)shape).Density));
            //            bc.AddToUIElementCollection(_parentChildrens);
            //            bc.RotoTranslation = RotoTranslation;
            //            yield return bc;
            //        }
            //    }




            //}
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
}
