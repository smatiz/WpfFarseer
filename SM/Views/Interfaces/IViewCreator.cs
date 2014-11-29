using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IViewCreator
    {
        void CreateFlag(FlagInfo flag);
        IBodyView CreateBody(BodyInfo body, IShapeViewCreator shapeCreator);
        IBreakableBodyView CreateBreakableBody(BodyInfo body, IShapeViewCreator shapeCreator);
        IJointView CreateJoint(IJoint joint);
    }
}
