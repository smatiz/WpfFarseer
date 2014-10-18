using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public struct float2 : IVector2
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
    }



    
}
