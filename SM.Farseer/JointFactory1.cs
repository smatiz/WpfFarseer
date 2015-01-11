
using System.Collections.Generic;
namespace SM.Farseer
{
    public partial class FarseerMaterialsCreator 
    {
        public IJointMaterial Create(JointInfo joint, IEnumerable<FlagInfo> flagInfos)
        {


            if(joint is RopeJointInfo)
            {
                return new RopeJointMaterial(_world, joint, flagInfos);
            }
        
    
            return null;
        }
    }
}

 