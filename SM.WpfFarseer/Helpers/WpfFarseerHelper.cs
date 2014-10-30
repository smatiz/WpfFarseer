using System.Collections.Generic;
using System.Linq;
using F = FarseerPhysics.Common;
using W = System.Windows;
using Xna = Microsoft.Xna.Framework;
using WShape = System.Windows.Shapes;
using FShape = FarseerPhysics.Collision.Shapes;
using SM;
using SM.Wpf;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;


namespace SM.WpfFarseer
{
    public static class WpfFarseerHelper
    {
        static uint[] GetData(System.Drawing.Bitmap img)
        {
            int n = img.Width * img.Height;
            var array = new uint[n];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    System.Drawing.Color pixel = img.GetPixel(i, j);
                    array[i * (img.Height - 1) + j] = pixel.R;
                }
            }

            return array;
        }

         public static uint[] GetData(this BitmapSource img)
        {
            int stride = 4 * img.PixelWidth;
            int size = stride * img.PixelWidth;
            var pixels = new uint[size];
            img.CopyPixels(pixels, stride, 0);
            return pixels;
        }


        public static System.Drawing.Bitmap ToBitmap(this UIElement element)
        {
            return element.ConvertToRenderTargetBitmap().ConvertToBitmap();
        }

        public static System.Drawing.Bitmap ConvertToBitmap(this RenderTargetBitmap target)
        {
            MemoryStream stream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(target));
            encoder.Save(stream);
            return new System.Drawing.Bitmap(stream);
        }

        #region To Wpf
        //public static System.Windows.Point ToWpf(this AForge.IntPoint c)
        //{
        //    return new System.Windows.Point(c.X, c.Y);
        //}
        //public static System.Windows.Shapes.Polygon ToWpfPolygon(this List<AForge.IntPoint> ps)
        //{
        //    var p = new System.Windows.Shapes.Polygon();
        //    foreach (var c in ps)
        //    {
        //        p.Points.Add(c.ToWpf());
        //    }
        //    return p;
        //}
        //public static System.Windows.Point ToWpf(this ClipperLib.IntPoint c)
        //{
        //    return new System.Windows.Point(c.X, c.Y);
        //}
        //public static System.Windows.Shapes.Polygon ToWpfPolygon(this List<ClipperLib.IntPoint> ps)
        //{
        //    var p = new System.Windows.Shapes.Polygon();
        //    foreach (var c in ps)
        //    {
        //        p.Points.Add(c.ToWpf());
        //    }
        //    return p;
        //}
        //public static List<System.Windows.Point> ToWpf(this List<ClipperLib.IntPoint> ps)
        //{
        //    var p = new List<System.Windows.Point>();
        //    foreach (var c in ps)
        //    {
        //        p.Add(c.ToWpf());
        //    }
        //    return p;
        //}
        #endregion



        private static F.Vertices ToFarseerVertices(this W.UIElement uielement, WShape.Polygon poly)
        {
            return new F.Vertices(from p in poly.Points select poly.TranslatePoint(p, uielement).ToFarseer());
        }

        public static F.Vertices ToFarseerVertices(this W.Media.PointCollection points)
        {
            return new F.Vertices(from p in points select p.ToFarseer());
        }
        public static F.Vertices ToFarseerVertices(this IEnumerable<SM.float2> points)
        {
            return new F.Vertices(from p in points select p.ToFarseer());
        }
        public static Xna.Vector2 ToFarseer(this SM.float2 p)
        {
            return new Xna.Vector2((float)p.X, (float)p.Y);
        }
        public static F.Vertices ToFarseerVertices(this W.UIElement uielement, WShape.Shape shape)
        {
            if (shape is W.Shapes.Polygon)
            {
                return uielement.ToFarseerVertices(shape as WShape.Polygon);
            }
            return null;
        }
        public static FShape.Shape ToFarseerShape(this W.UIElement uielement, WShape.Shape shape, float density = Consts.Density)
        {
            if (shape is W.Shapes.Polygon)
            {
                return new FShape.PolygonShape(uielement.ToFarseerVertices(shape as WShape.Polygon), density);
            }
            return null;
        }
        public static Xna.Vector2 ToFarseer(this W.Point p)
        {
            return new Xna.Vector2((float)p.X, (float)p.Y);
        }
        public static W.Point ToWpf(this Xna.Vector2 p)
        {
            return new W.Point(p.X, p.Y);
        }
        public static W.Media.PointCollection ToWpf(this F.Vertices ps)
        {
            var pointCollection = new W.Media.PointCollection();
            foreach(var p in ps)
            {
                pointCollection.Add(p.ToWpf());
            }
            return pointCollection;
        }

        public static WShape.Polygon ToWpfPolygon(this F.Vertices ps)
        {
            var poly = new WShape.Polygon();
            foreach (var p in ps)
            {
                poly.Points.Add(p.ToWpf());
            }
            return poly;
        }
        public static Xna.Vector2 GetOrigin(this W.FrameworkElement elem)
        {
            return WpfFarseerHelper.ToFarseer(elem.TranslatePoint(new System.Windows.Point(0, 0), (System.Windows.UIElement)elem.Parent));
        }
    }
}
