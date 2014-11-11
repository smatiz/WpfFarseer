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
    public class TriangulablePolygonShapeControl : BasicShapeControl, IBreakableShape, IPolygonsShape, IDrawable
    {
        Polygon _polygon;
        public TriangulablePolygonShapeControl()
        {
            _polygon = new Polygon();
        }


        public PointCollection PointCollection
        {
            get { return (PointCollection)GetValue(PointCollectionProperty); }
            set { SetValue(PointCollectionProperty, value); }
        }
        public static readonly DependencyProperty PointCollectionProperty =
            DependencyProperty.Register("PointCollection", typeof(PointCollection), typeof(TriangulablePolygonShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(PointCollectionPropertyChanged)));
        private static void PointCollectionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((TriangulablePolygonShapeControl)obj).OnPointCollectionChanged(); }
        private void OnPointCollectionChanged()
        {
            _polygon.Points = PointCollection;
        }

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(TriangulablePolygonShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(FillPropertyChanged)));
        private static void FillPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((TriangulablePolygonShapeControl)obj).OnFillChanged(); }
        private void OnFillChanged()
        {
            _polygon.Fill = Fill;
        }

        public IEnumerable<IEnumerable<float2>> PolygonShapes
        {
            get
            {

                return Helper.FarseerTools.Triangulate(PointCollection.Select(p => new float2((float)p.X, (float)p.Y)));
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
                return PolygonShapes.BBox(); 
            }
        }

        public IEnumerable<PointCollection> PointCollections
        {
            get 
            {
                return PolygonShapes.Select(x => x.ToWpf());
            }
        }
    }
}
