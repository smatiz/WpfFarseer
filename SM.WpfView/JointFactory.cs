
using System.Collections.Generic;
namespace SM.WpfView
{
    public partial class WpfViewsCreator 
    {
        public IJointView CreateJoint(JointInfo joint, IEnumerable<FlagInfo> flagInfos)
        {
            if(joint is RopeJointInfo)
            {
                return new RopeJointView(_rootCanvas, _context, (RopeJointInfo)joint, flagInfos);
            }
    
            return null;
        }
    }
}

 