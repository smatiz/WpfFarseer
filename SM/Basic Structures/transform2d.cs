using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public struct transform2d
    {
        readonly rotoTranslation _rotoTranslation;
        readonly float _zoom;
        public transform2d(rotoTranslation rotoTranslation, float zoom)
        {
            _rotoTranslation = rotoTranslation;
            _zoom = zoom;
        }

        public rotoTranslation RotoTranslation { get { return _rotoTranslation; } }
        public float Zoom { get { return _zoom; } }
    }
}
