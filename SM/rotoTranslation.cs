using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public struct rotoTranslation
    {
        readonly float2 _translation;
        readonly float _angle;
        public rotoTranslation(float2 translation, float angle)
        {
            _translation = translation;
            _angle = angle;
        }
        public float2 Translation { get { return _translation; } }
        public float Angle { get { return _angle; } }
    }
}
