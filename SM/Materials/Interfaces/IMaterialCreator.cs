using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IMaterialCreator
    {
        IMaterial Create(BodyInfo body, IShapeMaterialCreator shapeCreator);
        IJointMaterial Create(JointInfo joint, Info info);
    }
}
