using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public struct float2
    {
        readonly float _x;
        readonly float _y;
        public float2(float x, float y)
        {
            _x = x;
            _y = y;
        }
        public float X { get { return _x; } }
        public float Y { get { return _y; } }

        public override string ToString()
        {
            return String.Format("{0},{1}", _x, _y);
        }
        public static bool operator ==(float2 t1, float2 t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(float2 t1, float2 t2)
        {
            return !t1.Equals(t2);
        }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                return _x.GetHashCode() + _y.GetHashCode();
            }
        }
        public override bool Equals(object obj)
        {
            if (!(obj is float2)) return false;
            var t = (float2)obj;
            return t._x == _x && t._y == _y;
        }

        public static float2 Null { get { return new float2(0f, 0f); } }
    }
}
