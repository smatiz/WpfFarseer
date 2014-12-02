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

        public static transform2d Null { get { return new transform2d(rotoTranslation.Null, 1f); } }
    }

}
