using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM.AForgeClipperFarseer
{
    public static class Helper
    {

        public static List<List<ClipperLib.IntPoint>> GetBorder(this System.Drawing.Bitmap b, int n = 1)
        {
            var x = new FindBorder(b);
            return x.Process(n);
        }


        #region To Win Form
        public static System.Drawing.Point ToWinForm(this ClipperLib.IntPoint ps)
        {
            return new System.Drawing.Point((int)ps.X, (int)ps.Y);
        }

        public static List<System.Drawing.Point> ToWinForm(this List<ClipperLib.IntPoint> ps)
        {
            var p = new List<System.Drawing.Point>(ps.Count);
            for (int i = 0; i < ps.Count; i++)
            {
                p.Add(ps[i].ToWinForm());
            }
            return p;
        }
        public static System.Drawing.Point ToWinForm(this AForge.IntPoint ps)
        {
            return new System.Drawing.Point(ps.X, ps.Y);
        }
        #endregion



        #region To Farseer
        public static FarseerPhysics.Common.Vertices ToFarseer(this List<AForge.IntPoint> ps)
        {
           var vs = new FarseerPhysics.Common.Vertices();
            foreach (var x in ps)
            {
                vs.Add(x.ToFarseer());
            }
            return vs;
        }
        public static Microsoft.Xna.Framework.Vector2 ToFarseer(this AForge.IntPoint p)
        {
            return  new Microsoft.Xna.Framework.Vector2(p.X, p.Y);
        }

        public static FarseerPhysics.Common.Vertices ToFarseer(this List<ClipperLib.IntPoint> ps)
        {
            var vs = new FarseerPhysics.Common.Vertices();
            foreach (var x in ps)
            {
                vs.Add(x.ToFarseer());
            }
            return vs;
        }
        public static Microsoft.Xna.Framework.Vector2 ToFarseer(this ClipperLib.IntPoint p)
        {
            return new Microsoft.Xna.Framework.Vector2(p.X, p.Y);
        }
        #endregion
        #region To Aforge
        public static System.Drawing.Point ToAforge(this ClipperLib.IntPoint ps)
        {
            return new System.Drawing.Point((int)ps.X, (int)ps.Y);
        }
        #endregion
        #region To Clipper

        public static List<ClipperLib.IntPoint> ToClipper(this List<AForge.IntPoint> ps)
        {
            var vs = new List<ClipperLib.IntPoint>();
            foreach (var x in ps)
            {
                vs.Add(x.ToClipper());
            }
            return vs;
        }
        public static ClipperLib.IntPoint ToClipper(this  AForge.IntPoint p)
        {
            return new ClipperLib.IntPoint((int)p.X, (int)p.Y);
        }

        public static List<ClipperLib.IntPoint> ToClipper(this FarseerPhysics.Common.Vertices ps)
        {
            var vs = new List<ClipperLib.IntPoint>();
            foreach (var x in ps)
            {
                vs.Add(x.ToClipper());
            }
            return vs;
        }
        public static ClipperLib.IntPoint ToClipper(this  Microsoft.Xna.Framework.Vector2 p)
        {
            return new ClipperLib.IntPoint((int)p.X, (int)p.Y);
        }
        #endregion
    }
}
