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
        public rotoTranslation(float translationX, float translationY, float angle)
            : this(new float2(translationX, translationY), angle)
        {
        }
        public static rotoTranslation FromDegree(float2 translation, float angle)
        {
            return new rotoTranslation(translation, angle / AngleSubst);
        }
        public static rotoTranslation FromDegree(float translationX, float translationY, float angle)
        {
            return FromDegree(new float2(translationX, translationY), angle);
        }
        public float2 Translation { get { return _translation; } }
        public float Angle { get { return _angle; } }

        public float DegreeAngle { get { return AngleSubst * Angle; } }

        public override string ToString()
        {
            return String.Format("{0},r{1}", _translation, _angle);
        }
        public static rotoTranslation Null { get { return new rotoTranslation(float2.Null, 0f); } }
    }
}
