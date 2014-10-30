using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Wpf;
using SM.AForgeClipperFarseer;
using System.Windows.Media;

namespace SM.WpfFarseer
{
    class CanvasSkinner : ISkinnableCanvas
    {

        public List<List<System.Windows.Point>> FindBorder(VisualBrush brush, double w, double h, int n)
        {
            return brush.ConvertToRenderTargetBitmap(w, h).ConvertToBitmap().GetBorder().Select(x => x.ToWpf()).ToList();
        }
    }
}
