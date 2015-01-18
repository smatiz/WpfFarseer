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
        Action<CanvasId> _created;
        public BreakableBodyView(Action<CanvasId> created, IContext context, BodyInfo body, IShapeViewCreator shapeCreator)
            : base(created, context, body, shapeCreator)
        {
            _created = created;
        }

        public IEnumerable<IBodyView> BreakAndGetPieces(IEnumerable<BodyPieceMaterial> pieces)
        {
            Rect maxbb = Rect.Empty;
            foreach (var piece in pieces)
            {
                if (piece is PolygonPieceMaterial)
                {
                    maxbb.Union(((PolygonPieceMaterial)piece).Polygon.BBox());
                }
                else if (piece is CirclePieceMaterial)
                {
                    maxbb.Union(((CirclePieceMaterial)piece).Circle.BBox());
                }
            }
            var bodies = new List<BodyView>();
            _canvas.UpdateLayout();
            _rotation.Angle = 0;
            _traslation.X = 0;
            _traslation.Y = 0;
            var _visualBrush = new VisualBrush(_canvas);
            int index = 1;
            foreach (var piece in pieces)
            {
                if (piece is PolygonPieceMaterial)
                {
                    var ppiece = (PolygonPieceMaterial)piece;
                    var polygon = new System.Windows.Shapes.Polygon();
                    polygon.Points = ppiece.Polygon.ToWpf().Zoomed(Context.Zoom);
                    var vbClone = _visualBrush.Clone();
                    var polygonBB = ppiece.Polygon.BBox();
                    vbClone.Viewbox = new Rect((polygonBB.X - maxbb.X) / maxbb.Width, (polygonBB.Y - maxbb.Y) / maxbb.Height, polygonBB.Width / maxbb.Width, polygonBB.Height / maxbb.Height);

                    polygon.Fill = vbClone;
                    var polyShape = new PolygonShapeView(Context, polygon);

                    var bc = BodyView.Create(_created, this, piece.BodyMaterial.Id, polyShape);
                    bodies.Add(bc);
                }
                else if (piece is CirclePieceMaterial)
                {
                    var ppiece = (CirclePieceMaterial)piece;

                    var ellipse = new System.Windows.Shapes.Ellipse() { Width = ppiece.Circle.Radius * Context.Zoom * 2, Height = ppiece.Circle.Radius * Context.Zoom * 2 };
                    Canvas.SetLeft(ellipse, (ppiece.Circle.Center.X - ppiece.Circle.Radius) * Context.Zoom);
                    Canvas.SetTop(ellipse, (ppiece.Circle.Center.Y - ppiece.Circle.Radius)* Context.Zoom);
                  




                    //var polygon = new System.Windows.Shapes.Ellipse();
                    //polygon.Points = ppiece.Circle.ToWpf().Zoomed(Context.Zoom);
                    var vbClone = _visualBrush.Clone();
                    var polygonBB = ppiece.Circle.BBox();
                    vbClone.Viewbox = new Rect((polygonBB.X - maxbb.X) / maxbb.Width, (polygonBB.Y - maxbb.Y) / maxbb.Height, polygonBB.Width / maxbb.Width, polygonBB.Height / maxbb.Height);

                    ellipse.Fill = vbClone;
                    var ellipseShape = new CircleShapeView(Context, ellipse);

                    var bc = BodyView.Create(_created, this, piece.BodyMaterial.Id, ellipseShape);
                    bodies.Add(bc);
                }
                index++;
            }
            RemoveChild(_canvas);
            return bodies;
        }
    }
}
