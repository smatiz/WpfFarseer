 
namespace SM.WpfView
{
    public partial class WpfViewsCreator 
    { 
        public IJointView CreateJoint(JointInfo joint, Views views)
        {
 
            if(joint.Joint is IRopeJoint)
            {
                return new RopeJointView(_rootCanvas,_context, (IRopeJoint)joint.Joint, views);
            }
        
    
            return null;
        }
    }
}

 