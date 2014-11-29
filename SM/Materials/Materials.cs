using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class Materials
    {
        public IEnumerable<IBodyMaterial> Bodies { get; private set; }
        public IEnumerable<IBreakableBodyMaterial> BreakableBodies { get; private set; }
        public IEnumerable<IJointMaterial> Joints { get; private set; }

        public Materials(IMaterialCreator materiaCreator, IShapeMaterialCreator shapeCreator, Info info)
        {
            var bodies = new List<IBodyMaterial>();
            var breakablBodies = new List<IBreakableBodyMaterial>();
            foreach (var b in info.Bodies)
            {
                var br = materiaCreator.Create(b, shapeCreator);
                if (br is IBreakableBodyMaterial)
                {
                    breakablBodies.Add((IBreakableBodyMaterial)br);
                }
                else
                {
                    bodies.Add((IBodyMaterial)br);
                }
            }

            var joints = new List<IJointMaterial>();
            foreach (var j in info.Joints)
            {
                joints.Add(materiaCreator.Create(j));
            }

            Bodies = bodies;
            BreakableBodies = breakablBodies;
            Joints = joints;
        }
    }
}
