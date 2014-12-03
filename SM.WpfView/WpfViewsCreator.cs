using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.WpfView
{
    public class WpfViewsCreator : IViewCreator
    {
        RootView _rootView;
        public WpfViewsCreator(RootView rootView)
        {
            _rootView = rootView;
        }

        public IBodyView CreateBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            return new BodyView(_rootView, body, shapeCreator);
        }

        public IBreakableBodyView CreateBreakableBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            return new BreakableBodyView(_rootView, body, shapeCreator);
        }


        public IJointView CreateJoint(JointInfo joint, Views views)
        {
            if (joint.Joint is IRopeJoint)
            {
                return new RopeJointView(_rootView, (IRopeJoint)joint.Joint, views);
            }

            return null;
        }
    }
}
