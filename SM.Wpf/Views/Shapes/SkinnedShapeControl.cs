using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace SM.Wpf
{
    [ContentPropertyAttribute("Content")]
    public class SkinnedShapeControl : BasicShapeControl, IPolygonsShape, IDrawable, IBreakableShape
    {
        Canvas _canvas = new Canvas() { Background = new SolidColorBrush(Colors.Transparent) };//, Width = 1000, Height = 1000 };
     
        IEnumerable<IEnumerable<float2>> _polygonShapes;
        public SkinnedShapeControl()
        {
            refresh();
        }

        void refresh()
        {

            _canvas.RenderTransform = new ScaleTransform(_context.Zoom, _context.Zoom);
           
            //_canvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //_canvas.Arrange(new Rect(0, 0, _canvas.DesiredSize.Width, _canvas.DesiredSize.Height));
            //_canvas.UpdateLayout();

            try
            {
                if (Content != null)
                {
                    _canvas.Width = MaxWidth;
                    _canvas.Height = MaxHeight;
                    _canvas.Update();
                    _polygonShapes = Helper.FarseerTools.FindBorder(_canvas.ToVisualBrush(), MaxWidth, MaxHeight).Select(x => x.Select(p => new float2(p.X / _context.Zoom, p.Y / _context.Zoom)));

                    _canvas.Width = double.NaN;
                    _canvas.Height = double.NaN;
                    _canvas.Update();
                }
            }
            catch { }
        }


        public IEnumerable<IEnumerable<float2>> PolygonShapes
        {
            get
            {
                if(_polygonShapes == null)
                {
                    refresh();
                }
                return _polygonShapes;
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
            _canvas.Children.Clear();
            _canvas.Children.Add(Content);

            refresh();
        }

        public UIElement UIElement
        {
            get
            {
                return _canvas;
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
                return PolygonShapes.Select(x => new PointCollection(x.ToWpf().Select(p => new Point(p.X, p.Y))));
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
