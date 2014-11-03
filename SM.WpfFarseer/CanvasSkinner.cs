﻿using System;
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
    public class CanvasSkinner : __ISkinnableCanvas
    {
        public List<PointCollection> FindBorder(VisualBrush brush, double w, double h, int n)
        {
            var img = brush.ConvertToRenderTargetBitmap(w, h);
            uint[] us = img.GetData();
            var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
            return new List<PointCollection>(){ vs.ToWpf()};
        }

        public IEnumerable<IEnumerable<float2>> __FindBorder(VisualBrush brush, double w, double h)
        {
            var img = brush.ConvertToRenderTargetBitmap(w, h);
            uint[] us = img.GetData();
            var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
            var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
            ///vs.Holes
            return from x in vsp select x.ToSM();
            //return new List<IEnumerable<float2>>() { vs.ToSM() };
        }
    }
}
