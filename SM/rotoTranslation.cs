using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public struct rotoTranslation
    {
        const float AngleSubst = 180f / (float)Math.PI;
        readonly float2 _translation;
        readonly float _angle;
        public rotoTranslation(float2 translation, float angle)
        {
            _translation = translation;
            _angle = angle;
        }
        public static rotoTranslation FromDegree(float2 translation, float angle)
        {
            return new rotoTranslation(translation, angle / AngleSubst);
        }
        public float2 Translation { get { return _translation; } }
        public float Angle { get { return _angle; } }

        public float DegreeAngle { get { return AngleSubst * Angle; } }

        //public rotoTranslation Zoomed
        //{
        //    get
        //    {
        //        return new rotoTranslation(_translation.Zoomed, _angle);
        //    }
        //}
        //public rotoTranslation DeZoomed
        //{
        //    get
        //    {
        //        return new rotoTranslation(_translation.DeZoomed, _angle);
        //    }
        //}
    }
}
