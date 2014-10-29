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
    class AngleJointMaterial : BasicUnanchoredJointMaterial
    {
        AngleJoint __joint;

        public AngleJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

       protected override Joint Build(Body targetA, Body targetB)
        {
            __joint = JointFactory.CreateAngleJoint(_world, targetA, targetB);
            return __joint;
        }

        public float TargetAngle
        {
            get
            {
                return __joint.TargetAngle;
            }
            set
            {
                __joint.TargetAngle = value;
            }
        }

        public float BiasFactor
        {
            get
            {
                return __joint.BiasFactor;
            }
            set
            {
                __joint.BiasFactor = value;
            }
        }

        public float MaxImpulse
        {
            get
            {
                return __joint.MaxImpulse;
            }
            set
            {
                __joint.MaxImpulse = value;
            }
        }

        public float Softness
        {
            get
            {
                return __joint.Softness;
            }
            set
            {
                __joint.Softness = value;
            }
        }

    }
}
