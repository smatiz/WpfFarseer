
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace SM.Wpf
{
    /// <summary>
    /// Interaction logic for AroundBorderUserControl.xaml
    /// </summary>
    public static class Helper
    {
        public static IFarseerTools FarseerTools { get; set; }
        public static Point ToWpf(this float2 p)
        {
            return new Point(p.X,p.Y);
        }
        public static Polygon ToWpfPolygon(this IEnumerable<float2> ps)
        {
            var poly = new Polygon();
            foreach (var p in ps)
            {
                poly.Points.Add(p.ToWpf());
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
        public static void FillPolygons(this VisualBrush vb, Polygon[] ps)
        {
            Rect[] bbs = new Rect[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                bbs[i] = ps[i].BBox();
            }

            var bb = bbs[0];
            for (int i = 1; i < ps.Length; i++)
            {
                bb.Union(bbs[i]);
            }


            for (int i = 0; i < ps.Length; i++)
            {
                //var vb = new VisualBrush(uiElement);
                vb.AlignmentX = AlignmentX.Left;
                vb.AlignmentY = AlignmentY.Top;
                vb.Stretch = Stretch.None;
                vb.Viewport = new Rect((bb.X - bbs[i].X) / bbs[i].Width, (bb.Y - bbs[i].Y) / bbs[i].Height, 1, 1);
                ps[i].Fill = vb;
            }
        }

        public static VisualBrush GetVisualBrush(this UIElement element)
        {
            var brush = new VisualBrush(element);
            brush.Stretch = Stretch.None;
            brush.TileMode = TileMode.None;
            brush.AlignmentX = AlignmentX.Left;
            brush.AlignmentY = AlignmentY.Top;
            return brush;
        }


        public static RenderTargetBitmap ConvertToRenderTargetBitmap(this VisualBrush brush, double width, double height)
        {
            var target = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            var visual = new DrawingVisual();

            var drawingContext = visual.RenderOpen();
            drawingContext.DrawRectangle(brush, null, new Rect(new Point(0, 0), new Point(width, height)));
            drawingContext.Close();

            target.Render(visual);
            return target;
        }

        public static RenderTargetBitmap ConvertToRenderTargetBitmap(this UIElement element)
        {
            return element.GetVisualBrush().ConvertToRenderTargetBitmap(element.RenderSize.Width, element.RenderSize.Height);


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
    }
}
