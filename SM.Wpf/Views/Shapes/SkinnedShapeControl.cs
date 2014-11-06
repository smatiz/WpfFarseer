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
        public IEnumerable<IEnumerable<float2>> PolygonShapes
        {
            get
            {
                Content.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Content.Arrange(new Rect(0, 0, Content.DesiredSize.Width, Content.DesiredSize.Height));
                Content.UpdateLayout();
                return Helper.FarseerTools.FindBorder( Content.GetVisualBrush(), MaxWidth, MaxHeight);
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
            //Content.UpdateLayout();
        }

        public UIElement UIElement
        {
            get
            {
                return Content;
            }
        }
        public Rect BBox
        {
            get
            {
                return PolygonShapes.BBox();
            }
        }


        public IEnumerable<System.Windows.Shapes.Polygon> Polygons
        {
            get 
            {
                return PolygonShapes.Select(x => x.ToWpfPolygon());
            }
        }
    }
}
