using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public static class UnitSystem
    {

        static float M;
        static float V;
        public static float Zoom
        {
            set
            {
                V = value;
                M = 1 / V;
            }
        }
        public static float ToMaterial(this float f)
        {
            return f * M;
        }
        public static float ToView(this float f)
        {
            return f * V;
        }
        public static float2 ToMaterial(this float2 f)
        {
            return new float2(f.X.ToMaterial(), f.Y.ToMaterial());
        }
        public static float2 ToView(this float2 f)
        {
            return new float2(f.X.ToView(), f.Y.ToView());
        }
        public static rotoTranslation ToMaterial(this rotoTranslation r)
        {
            return new rotoTranslation(r.Translation.ToMaterial(), r.Angle.ToMaterial());
        }
        public static rotoTranslation ToView(this rotoTranslation r)
        {
            return new rotoTranslation(r.Translation.ToView(), r.Angle.ToView());
        }
    }
}
