
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace SM.WpfView
{
    public static class WpfHelper
    {
        public static Point ToWpf(this float2 p)
        {
            return new Point(p.X,p.Y);
        }
        public static Polygon ToWpfPolygon(this IEnumerable<float2> ps)
        {
            var poly = new Polygon();
            poly.Points = ps.ToWpf();
            return poly;
        }
        public static PointCollection ToWpf(this IEnumerable<float2> ps)
        {
            var poly = new PointCollection();
            foreach (var p in ps)
            {
                poly.Add(p.ToWpf());
            }
            return poly;
        }
        public static Rect BBox(this PointCollection ps)
        {
            Rect r = Rect.Empty;
            foreach (var x in ps)
            {
                r.Union(x);
            }
            return r;
        }
        public static Rect BBox(this Ellipse ellipse)
        {
            return ellipse.RenderedGeometry.Bounds;
        }
        public static Rect BBox(this Polygon ps)
        {
            return ps.Points.BBox();
        }
        public static Rect BBox(this IEnumerable<float2> poly)
        {
            Rect r = Rect.Empty;
            foreach (var p in poly)
            {
                r.Union(p.ToWpf());
            }
            return r;
        }
        public static Rect BBox(this IEnumerable<IEnumerable<float2>> polys)
        {
            Rect r = Rect.Empty;
            foreach (var poly in polys)
            {
                foreach (var p in poly)
                {
                    r.Union(p.ToWpf());
                }
            }
            return r;
        }
        public static Point Zoomed(this Point p, float z)
        {
            return new Point(p.X * z, p.Y * z);
        }
        public static Rect Zoomed(this Rect r, float z)
        {
            return new Rect(r.X * z, r.Y * z, r.Width * z, r.Height * z);
        }
        public static PointCollection Zoomed(this PointCollection ps, float z)
        {
            return new PointCollection(ps.Select(p => p.Zoomed(z)));
        }
        public static VisualBrush ToVisualBrush(this UIElement element)
        {
            var brush = new VisualBrush(element);
            brush.Stretch = Stretch.None;
            brush.TileMode = TileMode.None;
            brush.AlignmentX = AlignmentX.Left;
            brush.AlignmentY = AlignmentY.Top;
            return brush;
        }
        public static RenderTargetBitmap ToRenderTargetBitmap(this VisualBrush brush, double width, double height)
        {
            var target = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            var visual = new DrawingVisual();

            var drawingContext = visual.RenderOpen();
            drawingContext.DrawRectangle(brush, null, new Rect(new Point(0, 0), new Point(width, height)));
            drawingContext.Close();

            target.Render(visual);
            return target;
        }
        public static RenderTargetBitmap ToRenderTargetBitmap(this UIElement element)
        {
            return element.ToVisualBrush().ToRenderTargetBitmap(element.RenderSize.Width, element.RenderSize.Height);


            //return null;
            //element.UpdateLayout();
            //var target = new RenderTargetBitmap((int)(element.RenderSize.Width), (int)(element.RenderSize.Height), 96, 96, PixelFormats.Pbgra32);
            //var brush = new VisualBrush(element);
            //brush.Stretch = Stretch.None;
            //brush.TileMode = TileMode.None;
            //brush.AlignmentX = AlignmentX.Left;
            //brush.AlignmentY = AlignmentY.Top;
            //var visual = new DrawingVisual();
            
            //var drawingContext = visual.RenderOpen();


            //drawingContext.DrawRectangle(brush, null, new Rect(new Point(0, 0),
            //new Point(element.RenderSize.Width, element.RenderSize.Height)));

            //drawingContext.Close();

            //target.Render(visual);


            //return target;
        }
        public static void Update(this UIElement elem)
        {
            elem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            elem.Arrange(new Rect(new Point(), elem.DesiredSize));
            elem.UpdateLayout();
        }
    }
}
