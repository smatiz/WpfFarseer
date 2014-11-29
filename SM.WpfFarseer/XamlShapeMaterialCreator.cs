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

namespace SM.WpfFarseer
{
    public class XamlShapeMaterialCreator : IShapeMaterialCreator
    {
        const int EllipseNumberOfEdges = 10;
        IContext _context;

        public XamlShapeMaterialCreator(IContext context)
        {
            _context = context;
        }

        private IEnumerable<IEnumerable<float2>> FindBorder(object brush, double w, double h)
        {
            var img = ((VisualBrush)brush).ToRenderTargetBitmap(w, h);
            uint[] us = img.GetData();
            var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
            var vsp = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vs, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);
            return from x in vsp select x.ToSM();
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
                var points = PolygonTools.CreateEllipse((float)ellipse.RadiusX, (float)ellipse.RadiusY, EllipseNumberOfEdges).Select(p => new float2(p.X + ellipse.X, p.Y + ellipse.Y));
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
                var density = skinned.Density;

                var oldCanvas = (Canvas)skinned.Content.Parent;
                var canvas = new Canvas(){Background = new SolidColorBrush(Colors.Transparent)};
                if(oldCanvas != null)
                {
                    oldCanvas.Children.Remove(skinned.Content);
                }
                canvas.Children.Add(skinned.Content);

                IEnumerable<IEnumerable<float2>> polygonShapes;

                canvas.Width = skinned.MaxWidth;
                canvas.Height = skinned.MaxHeight;
                canvas.Update();
                polygonShapes = FindBorder(canvas.ToVisualBrush(), skinned.MaxWidth, skinned.MaxHeight).Select(x => x.Select(p => new float2(p.X / _context.Zoom, p.Y / _context.Zoom)));

                canvas.Width = double.NaN;
                canvas.Height = double.NaN;
                canvas.Update();

                polygons.AddRange(polygonShapes.Select(ps => new PolygonMaterial() { Density = skinned.Density, Points = ps.ToList() }));


                canvas.Children.Remove(skinned.Content);
                if (oldCanvas != null)
                {
                    oldCanvas.Children.Add(skinned.Content);
                }
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