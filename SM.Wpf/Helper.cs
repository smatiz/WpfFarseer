
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace SM.Wpf
{
    /// <summary>
    /// Interaction logic for AroundBorderUserControl.xaml
    /// </summary>
    public static class Helper 
    {
       

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
