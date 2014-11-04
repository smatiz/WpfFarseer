using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Farseer;
using SM.Wpf;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;

namespace SM.WpfFarseer
{
    public class FarseerTools : IFarseerTools
    {
        public IEnumerable<IEnumerable<float2>> FindBorder(VisualBrush brush, double w, double h)
        {
            var img = brush.ConvertToRenderTargetBitmap(w, h);
            uint[] us = img.GetData();
            var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
            var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
            ///vs.Holes
            return from x in vsp select x.ToSM();
            //return new List<IEnumerable<float2>>() { vs.ToSM() };
        }

        public IEnumerable<IEnumerable<float2>> Triangulate(IEnumerable<float2> poly)
        {
            var vs = poly.ToFarseerVertices();
            var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
            return from x in vsp select x.ToSM();
        }
    }
}
