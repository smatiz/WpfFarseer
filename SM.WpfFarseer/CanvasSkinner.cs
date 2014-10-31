using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Wpf;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;

namespace SM.WpfFarseer
{
    class CanvasSkinner : ISkinnableCanvas
    {
        public List<PointCollection> FindBorder(VisualBrush brush, double w, double h, int n)
        {
            var img = brush.ConvertToRenderTargetBitmap(w, h);
            uint[] us = img.GetData();
            var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
            return new List<PointCollection>(){ vs.ToWpf()};
        }
    }
}
