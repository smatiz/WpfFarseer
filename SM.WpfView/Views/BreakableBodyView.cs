using SM;
using SM.Wpf;
using SM.WpfView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SM.WpfView
{
    [ContentPropertyAttribute("Shapes")]
    public sealed class BreakableBodyView : BodyView, IBreakableBodyView
    {
        public BreakableBodyView(BasicView parent, BodyInfo body, IShapeViewCreator shapeCreator)
            : base(parent, body, shapeCreator)
        {

        }

        public IEnumerable<IBodyView> BreakAndGetPieces(IEnumerable<BodyPieceMaterial> pieces)
        {
            Rect maxbb = Rect.Empty;
            foreach (var piece in pieces)
            {
                maxbb.Union(piece.Polygon.BBox());
            }
            var bodies = new List<BodyView>();
            _canvas.Update();
            _rotation.Angle = 0;
            _traslation.X = 0;
            _traslation.Y = 0;
            var _visualBrush = new VisualBrush(_canvas);
            int index = 1;
            foreach (var piece in pieces)
            {
                var polygon = new System.Windows.Shapes.Polygon();
                polygon.Points = piece.Polygon.ToWpf().Zoomed(Context.Zoom);
                var vbClone = _visualBrush.Clone();
                var polygonBB = piece.Polygon.BBox();
                vbClone.Viewbox = new Rect((polygonBB.X - maxbb.X) / maxbb.Width, (polygonBB.Y - maxbb.Y) / maxbb.Height, polygonBB.Width / maxbb.Width, polygonBB.Height / maxbb.Height);

                polygon.Fill = vbClone;
                var polyShape = new PolygonShapeView(Context, polygon);

                var bc = BodyView.Create(this, String.Format("{0}_p{1}",Id , index), polyShape);
                bodies.Add(bc);
                index++;
            }
            RemoveChild(_canvas);
            return bodies;
        }
    }
}
