using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class BodyInfo : BasicInfo
    {
        public IEnumerable<IShape> Shapes { get; set; }
        public IEnumerable<IFlag> Flags { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }
        public BodyType BodyType { get; set; }

        public BodyInfo(IBody body)
        {
            Flags = body.Flags;
            X = body.X;
            Y = body.Y;
            Angle = body.Angle;
            BodyType = body.BodyType;

            Shapes = body.Shapes;
        }
    }
}
