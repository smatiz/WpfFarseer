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
    class RopeJointMaterial : IRopeJointMaterial
    {
        World _world;
        FarseerWorldManager _farseerWorldManager;
        RopeJoint _joint;

        public RopeJointMaterial(World world, FarseerWorldManager farseerWorldManager)
        {
            _world = world;
            _farseerWorldManager = farseerWorldManager;
        }

        public void Build(string id, string targetNameA, float2 anchorA, string targetNameB, float2 anchorB)
        {

            _joint = JointFactory.CreateRopeJoint(_world,
                _farseerWorldManager.FindObject<Body>(targetNameA),
                _farseerWorldManager.FindObject<Body>(targetNameB),
                anchorA.ToFarseer(), anchorB.ToFarseer());
        }

        public float2 AnchorA
        {
            get
            {
                return _joint.WorldAnchorA.ToFarseer();
            }
        }
        public float2 AnchorB
        {
            get
            {
                return _joint.WorldAnchorB.ToFarseer();
            }
        }
    }
}
