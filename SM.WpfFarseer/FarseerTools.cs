using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Farseer;
using SM.WpfView;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using System.Drawing;

namespace SM.WpfFarseer
{
    public class WpfFarseerTools 
    {
        //public IEnumerable<IEnumerable<float2>> FindBorder(object brush, double w, double h)
        //{
        //    var img = ((VisualBrush)brush).ToRenderTargetBitmap(w, h);
        //    //img.ConvertToBitmap().Save(@"C:\Users\Developer\Desktop\temp\aaaa.png");
        //    uint[] us = img.GetData();
        //    var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
        //    var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
        //    ///vs.Holes
        //    return from x in vsp select x.ToSM();
        //    //return new List<IEnumerable<float2>>() { vs.ToSM() };
        //}
        //public IEnumerable<IEnumerable<float2>> Triangulate(IEnumerable<float2> poly)
        //{
        //    var vs = poly.ToFarseerVertices();
        //    var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
        //    return from x in vsp select x.ToSM();
        //}
#if DEBUG
        public void Save(VisualBrush brush, string path)
        {

            brush.ToRenderTargetBitmap(1000, 1000).ToBitmap().Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
#endif

    }
}
