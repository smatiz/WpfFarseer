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

        //public IFlagView CreateFlag(FlagInfo body)
        //{
        //    return new FlagView( body);
        //}

        public IBodyView CreateBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            return new BodyView(_rootView, body, shapeCreator);
        }

        public IBreakableBodyView CreateBreakableBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            return new BreakableBodyView(_rootView, body, shapeCreator);
        }


        public IJointView CreateJoint(JointInfo joint)
        {
            return new RopeJointView(_rootView, (IRopeJoint)joint.Joint);
        }
    }
}
