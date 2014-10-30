using ClipperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.AForgeClipperFarseer
{
    public class FindBorder
    {
        System.Drawing.Bitmap _bitmap;
        List<List<AForge.IntPoint>> _polys;
        public List<List<List<AForge.IntPoint>>> _polysAll = new List<List<List<AForge.IntPoint>>>();

        public FindBorder(System.Drawing.Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        private void Process()
        {
            _polys = getGrahamConvexHull(_bitmap);
            _polysAll.Add(_polys);
            FindBorder.Negative(_bitmap, _polys.Where(p => p.Count > 3).Select(p =>
                {
                    var offset = new ClipperOffset();
                    offset.AddPath(p.ToClipper(), JoinType.jtSquare, EndType.etClosedPolygon);
                    var sol = new List<List<ClipperLib.IntPoint>>();
                    offset.Execute(ref sol, 1);
                    var ps = sol.First().ToFarseer();
                    //var ps = p.ToClipper().ToFarseer();
                    //ps.Translate(new Microsoft.Xna.Framework.Vector2());
                    return ps;
                }).ToArray());
        }

        static int i = 1;
        private static List<List<ClipperLib.IntPoint>> Substract(List<List<ClipperLib.IntPoint>> poly1, List<List<ClipperLib.IntPoint>> poly2)
        {
            string name = "bbb" + i.ToString();
            i++;
            BitmapDrawerManager.Path = @"C:\Users\Developer\Desktop\TEMP\";
            BitmapDrawerManager.Size = new System.Drawing.Size(1000, 1000);
            

            var solution = new PolyTree();
            var c = new Clipper();
            bool b;
            b = c.AddPaths(poly1, PolyType.ptSubject, true);
            b = c.AddPaths(poly2, PolyType.ptClip, true);
            b = c.Execute(ClipType.ctDifference, solution);

            foreach (var p in poly1)
            {
                D.Graphics(name).FillPolygon(D.Brush(System.Drawing.Color.Red, 50), p.ToWinForm().ToArray());
            }
            D.Commit(name);
            foreach (var p in poly2)
            {
                D.Graphics(name).FillPolygon(D.Brush(System.Drawing.Color.Blue, 50), p.ToWinForm().ToArray());
            }
            D.Commit(name);
            foreach (var p in solution.Childs)
            {
                D.Graphics(name).FillPolygon(D.Brush(System.Drawing.Color.Green, 50), p.Contour.ToWinForm().ToArray());
            }
            D.Commit(name);
            foreach (var p in solution.Childs)
            {
                D.Graphics(name).DrawPolygon(D.Pen(System.Drawing.Color.Black, 255), p.Contour.ToWinForm().ToArray());
            }

            D.Save(name);
            return solution.Childs.Select(x=> x.Contour).ToList();
        }

        public List<List<ClipperLib.IntPoint>> Process(int n)
        {
            int n2 = n * 2 - 1;
            for (int i = 0; i < n2; i++)
                Process();


            var clipperPoly = _polysAll.Select(poly =>
            {
                return poly.Select(ps =>
                {
                    return ps.Select(p =>
                    {
                        return new ClipperLib.IntPoint(p.X, p.Y);
                    }).ToList();
                }).ToList();
            }).ToArray();


            var solution = new List<List<IntPoint>>();
            solution = clipperPoly.Last();

            for (int i = clipperPoly.Length - 2; i >= 0; i--)
            {
                var currentSubstracter = solution;
                solution = Substract(clipperPoly[i], currentSubstracter);
            }
            return solution;
        }

        private static List<List<AForge.IntPoint>> getGrahamConvexHull(System.Drawing.Bitmap image)
        {
            List<List<AForge.IntPoint>> list = new List<List<AForge.IntPoint>>();
            var blobCounter = new AForge.Imaging.BlobCounter();
            blobCounter.ProcessImage(image);
            var blobs = blobCounter.GetObjectsInformation();

            var hullFinder = new AForge.Math.Geometry.GrahamConvexHull();

            foreach (var blob in blobs)
            {
                List<AForge.IntPoint> leftPoints, rightPoints;
                var edgePoints = new List<AForge.IntPoint>();

                blobCounter.GetBlobsLeftAndRightEdges(blob, out leftPoints, out rightPoints);

                edgePoints.AddRange(leftPoints);
                edgePoints.AddRange(rightPoints);

                var hull = hullFinder.FindHull(edgePoints);
                list.Add(hull);
            }
            return list;
        }

        // http://csharpexamples.com/fast-image-processing-c/
        private static void ProcessUsingLockbitsAndUnsafeAndParallel(System.Drawing.Bitmap processedBitmap, Action<int, int, byte[]> func)
        {
            unsafe
            {
                System.Drawing.Imaging.BitmapData bitmapData = processedBitmap.LockBits(new System.Drawing.Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        byte[] color = new byte[bytesPerPixel];
                        for (int i = 0; i < bytesPerPixel; i++)
                        {
                            color[i] = currentLine[x + i];
                        }

                        func(x / bytesPerPixel, y, color);

                        for (int i = 0; i < bytesPerPixel; i++)
                        {
                            currentLine[x + i] = color[i];
                        }
                    }
                });
                processedBitmap.UnlockBits(bitmapData);
            }
        }

        private static void Negative(System.Drawing.Bitmap bitmap, IEnumerable<FarseerPhysics.Common.Vertices> vss)
        {
            //var bitmap2 = new System.Drawing.Bitmap(bitmap.Width, bitmap.Height);


            ProcessUsingLockbitsAndUnsafeAndParallel(bitmap, (i, j, data) =>
            {
                var p = new Microsoft.Xna.Framework.Vector2(i, j);
                bool flag = false;
                bool isTransparent = data[3] == 0;
                if (isTransparent)
                {
                    bool isInsideOne = false;
                    foreach (var vs in vss)
                    {
                        if (vs.PointInPolygon(ref p) == 1)
                        {
                            isInsideOne = true;
                            break;
                        }
                    }
                    if (isInsideOne)
                    {
                        flag = true;
                    }
                }

                if(flag)
                {
                    data[0] = 255;
                    data[1] = 0;
                    data[2] = 0;
                    data[3] = 255;
                }
                else
                {
                    data[0] = 0;
                    data[1] = 0;
                    data[2] = 0;
                    data[3] = 0;
                }
            });

            //bitmap.Save(@"C:\Users\Developer\Desktop\TEMP\ooooo.png", System.Drawing.Imaging.ImageFormat.Png);
            //for (int i = 0; i < bitmap.Width; i++)
            //{
            //    for (int j = 0; j < bitmap.Height; j++)
            //    {
            //        System.Drawing.Color c = System.Drawing.Color.FromArgb(0, 0, 0, 0);
            //        bool isTransparent = bitmap.GetPixel(i, j).A == 0;
            //        if (isTransparent)
            //        {
            //            bool isInsideOne = false;
            //            foreach (var vs in vss)
            //            {
            //                var p = new Microsoft.Xna.Framework.Vector2(i, j);
            //                if (vs.PointInPolygon(ref p) == 1)
            //                {
            //                    isInsideOne = true;
            //                    break;
            //                }
            //            }
            //            if (isInsideOne)
            //            {
            //                c = System.Drawing.Color.FromArgb(255, 255, 0, 0);
            //            }
            //        }

            //        bitmap2.SetPixel(i, j, c);
            //    }
            //}
            //return bitmap2;
        }
    }
}
