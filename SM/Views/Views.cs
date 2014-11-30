using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class Views
    {
        public IEnumerable<IBodyView> Bodies { get; private set; }
        public IEnumerable<IBreakableBodyView> BreakableBodies { get; private set; }
        public IEnumerable<IJointView> Joints { get; private set; }

        public Views(IViewCreator viewCreator, IShapeViewCreator shapeCreator, Info info)
        {
            var bodies = new List<IBodyView>();
            foreach (var b in info.Bodies.Where(b => b.BodyType != BodyType.Breakable))
            {
                var bv = viewCreator.CreateBody(b, shapeCreator);
                bodies.Add(bv);
            }
            Bodies = bodies;

            var breakableBodies = new List<IBreakableBodyView>();
            foreach (var b in info.Bodies.Where(b => b.BodyType == BodyType.Breakable))
            {
                breakableBodies.Add(viewCreator.CreateBreakableBody(b, shapeCreator));
            }
            BreakableBodies = breakableBodies;

            var joints = new List<IJointView>();
            foreach (var j in info.Joints)
            {
                joints.Add(viewCreator.CreateJoint(j));
            }
            Joints = joints;
        }


    }
}
