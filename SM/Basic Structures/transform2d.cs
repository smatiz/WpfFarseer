using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    [TypeConverter(typeof(Transform2dConverter))]
    public struct transform2d
    {
        readonly rotoTranslation _rotoTranslation;
        readonly float _scale;
        public transform2d(rotoTranslation rotoTranslation, float zoom)
        {
            _rotoTranslation = rotoTranslation;
            _scale = zoom;
        }
        public transform2d(float translationX, float translationY, float angle, float scale)
            : this(new rotoTranslation(translationX, translationY, angle), scale)
        {
        }

        public rotoTranslation RotoTranslation { get { return _rotoTranslation; } }
        public float Scale { get { return _scale; } }

        public override string ToString()
        {
            return String.Format("{0},{1}", _rotoTranslation, _scale);
        }

        public static bool operator ==(transform2d t1, transform2d t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(transform2d t1, transform2d t2)
        {
            return !t1.Equals(t2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is transform2d)) return false;
            var t = (transform2d)obj;
            return t._scale == _scale && t._rotoTranslation == _rotoTranslation;
        }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                return _scale.GetHashCode() + _rotoTranslation.GetHashCode();
            }
        }

        public static transform2d operator *(transform2d t1, transform2d t2)
        {
            return (t1.ToMatrix() * t2.ToMatrix()).ToTransform2d();
        }

        public static transform2d Null { get { return new transform2d(rotoTranslation.Null, 1f); } }
    }
}
