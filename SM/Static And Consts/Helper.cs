using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public static class Helper
    {

        public static Matrix ToMatrix(this transform2d t)
        {
            var c = (float)Math.Cos(t.RotoTranslation.Angle);
            var s = (float)Math.Sin(t.RotoTranslation.Angle);
            return Matrix.Build(3,
                c, -s, t.RotoTranslation.Translation.X,
                s, c, t.RotoTranslation.Translation.Y,
                0, 0, 1.0f / t.Scale
                );
        }
        private static float GetAngle(float c, float s)
        {
            if (Math.Abs(c) < Consts.Epsilon)
            {
                return (float)Math.PI * 0.5f * (s > 0 ? 1f : -1f);
            }
            else
            {
                return (float)Math.Atan(s / c) + (float)Math.PI * (c > 0 ? 0f : 1f);
            }
        }
        public static transform2d ToTransform2d(this Matrix m)
        {
            return new transform2d(m.mat[0, 2], m.mat[1, 2], GetAngle(m.mat[0, 0], m.mat[1, 0]), 1.0f / m.mat[2, 2]);
        }



        public static FlagInfo FindFlagInfo(this IEnumerable<FlagInfo> flagInfos, string name)
        {
            foreach (var f in flagInfos)
            {
                if (f.Id == name)
                {
                    return f;
                }
            }
            return null;
        }
    }

    //    static float _zoom;
    //    static float _dezoom;
    //    static Helper()
    //    {
    //        _zoom = 10;
    //        _dezoom = 1 / _zoom;
    //    }

    //    public static float Zoom
    //    {
    //        set
    //        {
    //            _zoom = value;
    //            _dezoom = 1 / _zoom;
    //        }
    //    }


    //    public static float Zoomed(this float f)
    //    {
    //        return _zoom * f;
    //    }
    //    public static float DeZoomed(this float f)
    //    {
    //        return _dezoom * f;
    //    }
    //    public static double Zoomed(this double f)
    //    {
    //        return _zoom * f;
    //    }
    //    public static double DeZoomed(this double f)
    //    {
    //        return _dezoom * f;
    //    }
    //    public static float2 Zoomed(this float2 f)
    //    {
    //        return new float2(f.X.Zoomed(), f.Y.Zoomed());
    //    }
    //    public static float2 DeZoomed(this float2 f)
    //    {
    //        return new float2(f.X.DeZoomed(), f.Y.DeZoomed());
    //    }
    //    public static rotoTranslation Zoomed(this rotoTranslation f)
    //    {
    //        return new rotoTranslation(f.Translation.Zoomed(), f.Angle.Zoomed());
    //    }
    //    public static rotoTranslation DeZoomed(this rotoTranslation f)
    //    {
    //        return new rotoTranslation(f.Translation.DeZoomed(), f.Angle.DeZoomed());
    //    }
    //}
}
