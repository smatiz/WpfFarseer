using FarseerPhysics.Common;
using SM.Xaml;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using SM.WpfView;
using System.Windows;

namespace SM.WpfFarseer
{
    public class XamlShapeMaterialCreator : IShapeMaterialCreator
    {

        const double DefaultWidth = 3000;
        const double DefaultHeight = DefaultWidth;

        IContext _context;

        public XamlShapeMaterialCreator(IContext context)
        {
            _context = context;
        }

        private IEnumerable<IEnumerable<float2>> FindBorder(Canvas canvas, double precisionZoom) //object brush, double w, double h)
        {
            IEnumerable<IEnumerable<float2>> result;

            var oldCanvas = (Canvas)canvas.Parent;
            var containerCanvas = new Canvas() { Background = new SolidColorBrush(Colors.Transparent) };//.FromArgb(1,0,0,0)) };
            if (oldCanvas != null)
            {
                oldCanvas.Children.Remove(canvas);
            }
            containerCanvas.Children.Add(canvas);

            containerCanvas.Width = DefaultWidth;
            containerCanvas.Height = DefaultHeight;
            containerCanvas.ForceUpdateLayout();

            Rect box = Rect.Empty;

            {
                var img = containerCanvas.ToVisualBrush().ToRenderTargetBitmap(DefaultWidth, DefaultHeight);
                //img.ToBitmap().Save(@"C:\Users\Developer\Desktop\aaaa.png");
                uint[] us = img.GetData();
                foreach(var p in FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width))
                {
                    box.Union(p.ToWpf());
                }
            }

            containerCanvas.Width = box.Width;
            containerCanvas.Height = box.Height;
            containerCanvas.RenderTransform = new ScaleTransform(precisionZoom, precisionZoom);
            containerCanvas.ForceUpdateLayout();

            {
                var rW = precisionZoom * (box.Width + (1 + box.X));
                var rH = precisionZoom * (box.Height + (1 + box.Y));

                var img = containerCanvas.ToVisualBrush().ToRenderTargetBitmap(rW, rH);
                //img.ToBitmap().Save(@"C:\Users\Developer\Desktop\bbb.png");
                uint[] us = img.GetData();
                var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
                var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
                result = vsp.Select(ps => ps.Select(p => new float2((float)(p.X / precisionZoom), (float)(p.Y / precisionZoom))));
                    //from x in vsp select x.ToSM();
            }

            containerCanvas.RenderTransform = null;
            containerCanvas.Width = double.NaN;
            containerCanvas.Height = double.NaN;
            containerCanvas.ForceUpdateLayout();

            containerCanvas.Children.Remove(canvas);
            if (oldCanvas != null)
            {
                oldCanvas.Children.Add(canvas);
            }

            return result;
        }
        private void addTo(object shape, List<CircleMaterial> circles, List<PolygonMaterial> polygons)
        {

            var circle = shape as Circle;
            if (circle != null)
            {
                circles.Add(new CircleMaterial() { Density = circle.Density, Radius = circle.Radius, Center = new float2(circle.X, circle.Y) });
                return;
            }

            var ellipse = shape as Ellipse;
            if (ellipse != null)
            {
                var density = ellipse.Density;
                var points = PolygonTools.CreateEllipse((float)ellipse.RadiusX, (float)ellipse.RadiusY, _context.EllipseNumberOfEdges).Select(p => new float2(p.X + ellipse.X, p.Y + ellipse.Y));
                polygons.Add(new PolygonMaterial() { Density = ellipse.Density, Points = points.ToList() });
                return;
            }

            var polygon = shape as Polygon;
            if (polygon != null)
            {
                var density = polygon.Density;
                IEnumerable<IEnumerable<float2>> polygonShapes;
                if (polygon.Convex)
                {
                    polygonShapes = new IEnumerable<float2>[] { polygon.Points.Select(p => new float2((float)p.X, (float)p.Y)) };
                }
                else
                {
                    var vs = polygon.Points.ToFarseerVertices();
                    var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
                    polygonShapes = from x in vsp select x.ToSM();
                }
                polygons.AddRange(polygonShapes.Select(ps => new PolygonMaterial() { Density = polygon.Density, Points = ps.ToList() }));
                return;
            }

            var skinned = shape as SkinnedShape;
            if (skinned != null)
            {
                var polygonShapes = FindBorder(skinned.Content, skinned.PrecisionZoom).Select(x => x.Select(p => new float2(p.X, p.Y)));
                polygons.AddRange(polygonShapes.Select(ps => new PolygonMaterial() { Density = skinned.Density, Points = ps.ToList() }));
                return;
            }
        }

        public ShapesMaterial Create(IEnumerable<IShape> shapes)
        {
            List<CircleMaterial> circles = new List<CircleMaterial>();
            List<PolygonMaterial> polygons = new List<PolygonMaterial>();
            foreach (var shape in shapes)
            {
                addTo(shape, circles, polygons);
            }
            return new ShapesMaterial(circles, polygons);
        }
    }
}