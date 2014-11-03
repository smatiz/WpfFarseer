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
        Dictionary<UIElement, Rect> _map = new Dictionary<UIElement, Rect>();
        //public VisualBrushFiller(VisualBrush brush)
        //{
        //    _brush = brush;
        //}

        public void Add(Polygon polygon)
        {
            _map.Add(polygon, polygon.BBox());
        }

        public void Add(System.Windows.Shapes.Ellipse ellipse)
        {
            //ellipse.
            //_map.Add(polygon, polygon.BBox());
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

        public VisualBrush GetBrush(Polygon polygon, UIElement uiElement)
        {
            var maxbb = ComputeBBox();
            var polygonBB = _map[polygon];
            var vb = new VisualBrush(uiElement);
            vb.AlignmentX = AlignmentX.Left;
            vb.AlignmentY = AlignmentY.Top;
            vb.Stretch = Stretch.None;
            vb.Viewport = new Rect((maxbb.X - polygonBB.X) / polygonBB.Width, (maxbb.Y - polygonBB.Y) / polygonBB.Height, 1, 1); 
            //new Rect((bb.X - bbs.X) / bbs.Width, -(bb.Y - bbs.Y) / bbs.Height, 1, 1);
            return vb;
        }
    }
}
