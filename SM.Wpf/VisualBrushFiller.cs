using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.Wpf
{
    public class VisualBrushFiller
    {
        //VisualBrush _brush;
        Dictionary<IBoxed, Rect> _map = new Dictionary<IBoxed, Rect>();
        //public VisualBrushFiller(VisualBrush brush)
        //{
        //    _brush = brush;
        //}

        public void Add(IBoxed boxed)
        {
            _map.Add(boxed, boxed.BBox);
        }

        private Rect ComputeBBox()
        {
            Rect[] bboxes = _map.Values.ToArray();
            var bb = bboxes[0];
            for (int i = 1; i < bboxes.Length; i++)
            {
                bb.Union(bboxes[i]);
            }
            return bb;
        }

        public VisualBrush GetBrush(IBoxed boxed, UIElement uiElement)
        {
            var maxbb = ComputeBBox();
            var polygonBB = _map[boxed];
            var vb = new VisualBrush(uiElement);
            vb.Viewbox = new Rect(-(maxbb.X - polygonBB.X) / maxbb.Width, -(maxbb.Y - polygonBB.Y) / maxbb.Height, polygonBB.Width / maxbb.Width, polygonBB.Height / maxbb.Height);
            //vb.AlignmentX = AlignmentX.Left;
            //vb.AlignmentY = AlignmentY.Top;
            //vb.Stretch = Stretch.None;
            //vb.Viewport = new Rect((maxbb.X - polygonBB.X) / polygonBB.Width, (maxbb.Y - polygonBB.Y) / polygonBB.Height, 1, 1); 
            ////new Rect((bb.X - bbs.X) / bbs.Width, -(bb.Y - bbs.Y) / bbs.Height, 1, 1);
            return vb;


           // vb.Clone
        }
    }
}
