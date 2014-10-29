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
    class MotorJointMaterial : BasicUnanchoredJointMaterial
    {

        MotorJoint __joint;

        public MotorJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Body targetB)
        {
            __joint = JointFactory.CreateMotorJoint(_world, targetA, targetB);
            return __joint;
        }

    }
}
