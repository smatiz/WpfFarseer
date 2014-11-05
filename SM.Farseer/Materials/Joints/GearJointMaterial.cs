using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    class GearJointMaterial : BasicJointMaterial
    {

        GearJoint __joint;

        public GearJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }


        public void Build(string id, string targetNameA, string targetNameB, string jointNameA, string jointNameB, float ratio)
        {
            _id = id;
            _joint = Build(_farseerWorldManager.FindObject<Body>(targetNameA), _farseerWorldManager.FindObject<Body>(targetNameB),
                _farseerWorldManager.FindObject<Joint>(jointNameA), _farseerWorldManager.FindObject<Joint>(jointNameB),
                ratio);
        }

        Joint Build(Body targetA, Body targetB, Joint jointA, Joint jointB, float ratio)
        {
            __joint = JointFactory.CreateGearJoint(_world, targetA, targetB,jointA, jointB, ratio);
            return __joint;
        }

        public float Ratio
        {
            get
            {
                return __joint.Ratio;
            }
            set
            {
                __joint.Ratio = value;
            }
        }
    }
}
