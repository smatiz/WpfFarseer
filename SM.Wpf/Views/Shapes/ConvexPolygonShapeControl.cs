using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.Wpf
{
    [ContentPropertyAttribute("PointCollection")]
    public class ConvexPolygonShapeControl : BasicShapeControl, IPolygonShape, IDrawable, IBreakableShape
    {

        Polygon _polygon;
        public ConvexPolygonShapeControl()
        {
            _polygon = new Polygon();
            //_polygon.Stroke = new SolidColorBrush(Colors.Black);
            //_polygon.StrokeThickness = 1;
        }

        //public __ConvexPolygonShapeControl__(Polygon poly, float density)
        //    : this()
        //{
        //    _polygon = poly;
        //    Density = density;
        //}





        public PointCollection PointCollection
        {
            get { return (PointCollection)GetValue(PointCollectionProperty); }
            set { SetValue(PointCollectionProperty, value); }
        }
        public static readonly DependencyProperty PointCollectionProperty =
            DependencyProperty.Register("PointCollection", typeof(PointCollection), typeof(ConvexPolygonShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(PointCollectionPropertyChanged)));
        private static void PointCollectionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((ConvexPolygonShapeControl)obj).OnPointCollectionChanged(); }
        private void OnPointCollectionChanged()
        {
            refresh();
        }



        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(ConvexPolygonShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(FillPropertyChanged)));
        private static void FillPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((ConvexPolygonShapeControl)obj).OnFillChanged(); }
        private void OnFillChanged()
        {
            _polygon.Fill = Fill;

        }



        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(ConvexPolygonShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(StrokePropertyChanged)));
        private static void StrokePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((ConvexPolygonShapeControl)obj).OnStrokeChanged(); }
        private void OnStrokeChanged()
        {
            _polygon.Stroke = Stroke;
        }



        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(ConvexPolygonShapeControl), new PropertyMetadata(0.0, new PropertyChangedCallback(StrokeThicknessPropertyChanged)));
        private static void StrokeThicknessPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((ConvexPolygonShapeControl)obj).OnStrokeThicknessChanged(); }
        private void OnStrokeThicknessChanged()
        {
            _polygon.StrokeThickness = StrokeThickness;
        }

        public IEnumerable<float2> Points
        {
            get
            {
                return PointCollection.Select(p => new float2((float)p.X, (float)p.Y));
            }
        }

        public UIElement UIElement
        {
            get
            {
                return _polygon;
            }
        }

        public Rect BBox
        {
            get
            {
                return PointCollection.BBox(); 
            }
        }

        void refresh()
        {
            if (PointCollection != null)
            {
                _polygon.Points.Clear();
                foreach (var p in PointCollection)
                {
                    _polygon.Points.Add(p.Zoomed(_context.Zoom));
                }
            }
        }

        public IEnumerable<PointCollection> PointCollections
        {
            get
            {
                yield return PointCollection;
            }
        }

        public override IContext Context
        {
            set
            {
                base.Context = value;
                refresh();
            }
        }
    }
}
