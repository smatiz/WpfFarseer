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
    class AngleJointMaterial : BasicJointMaterial
    {
        AngleJoint __joint;

        public AngleJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        public void Build(string id, string targetNameA, string targetNameB)
        {
            _id = id;
            __joint = Build(_farseerWorldManager.FindObject<Body>(targetNameA), _farseerWorldManager.FindObject<Body>(targetNameB));
        }

        private AngleJoint Build(Body targetA, Body targetB)
        {
            return JointFactory.CreateAngleJoint(_world, targetA, targetB);
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
