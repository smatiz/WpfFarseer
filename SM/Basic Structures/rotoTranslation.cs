using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    [TypeConverter(typeof(RotoTranslationConverter))]
    public struct rotoTranslation
    {
        const float AngleSubst = 180f / (float)Math.PI;
        readonly float2 _translation;
        readonly float _angle;
        public rotoTranslation(float2 translation, float angle)
        {
            _translation = translation;

            var _360 = 2f * (float)Math.PI;
            _angle = angle;
            while (_angle < 0)
            {
                _angle += _360;
            }
            while (_angle > _360)
            {
                _angle -= _360;
            }
            if ((_360 - _angle) < Consts.Epsilon)
            {
                _angle = 0;
            }
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
        public static bool operator ==(rotoTranslation t1, rotoTranslation t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(rotoTranslation t1, rotoTranslation t2)
        {
            return !t1.Equals(t2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is rotoTranslation)) return false;
            var t = (rotoTranslation)obj;
            return Math.Abs(t._angle - _angle) < Consts.Epsilon && t._translation == _translation;
        }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                return _angle.GetHashCode() + _translation.GetHashCode();
            }
        }
        public static rotoTranslation Null { get { return new rotoTranslation(float2.Null, 0f); } }
    }
}
