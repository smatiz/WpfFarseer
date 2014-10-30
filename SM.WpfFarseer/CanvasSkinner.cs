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
            //return brush.ConvertToRenderTargetBitmap(w, h).ConvertToBitmap().GetBorder().Select(x => x.ToWpf()).ToList();

            var v = new Vertices();
            var img = brush.ConvertToRenderTargetBitmap(w, h);
            //img.ConvertToBitmap().Save(@"C:\Users\Developer\Desktop\temp\aaaaaaaaa.png", System.Drawing.Imaging.ImageFormat.Png);


            uint[] us = img.GetData();
            //for (int i = 0; i < n; i++)
            //{
            //    us[i] = (uint)bs[i * 4];
            //}
            var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);

            return new List<PointCollection>(){ vs.ToWpf()};
            //System.Windows.Shapes.Polygon poly = vs.ToWpfPolygon();
            //poly.Stroke = new SolidColorBrush(Colors.CadetBlue);
        }
    }
}
