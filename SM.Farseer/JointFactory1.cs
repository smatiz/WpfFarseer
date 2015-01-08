
namespace SM.Farseer
{
    public partial class FarseerMaterialsCreator 
    {
        public IJointMaterial Create(JointInfo joint, Info info)
        {


            if(joint.Joint is IRopeJoint)
            {
                return new RopeJointMaterial(_world, joint, info);
            }
        
    
            return null;
        }
    }
}

 