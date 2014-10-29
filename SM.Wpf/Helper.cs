
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
        public static System.Drawing.Bitmap ToBitmap(this UIElement element)
        {
            return element.ConvertToRenderTargetBitmap().ConvertToBitmap();
        }

        public static System.Drawing.Bitmap ConvertToBitmap(this RenderTargetBitmap target)
        {
            MemoryStream stream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(target));
            encoder.Save(stream);
            return new System.Drawing.Bitmap(stream);
        }

        public static RenderTargetBitmap ConvertToRenderTargetBitmap(this UIElement element)
        {
            element.UpdateLayout();
            var target = new RenderTargetBitmap((int)(element.RenderSize.Width), (int)(element.RenderSize.Height), 96, 96, PixelFormats.Pbgra32);
            var brush = new VisualBrush(element);
            brush.Stretch = Stretch.None;
            brush.TileMode = TileMode.None;
            brush.AlignmentX = AlignmentX.Left;
            brush.AlignmentY = AlignmentY.Top;
            var visual = new DrawingVisual();
            
            var drawingContext = visual.RenderOpen();


            drawingContext.DrawRectangle(brush, null, new Rect(new Point(0, 0),
            new Point(element.RenderSize.Width, element.RenderSize.Height)));

            drawingContext.Close();

            target.Render(visual);


            return target;
        }
    }
}
