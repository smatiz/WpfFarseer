using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class WeldJointMaterial : BasicTwoPointJointMaterial, IDistanceJointMaterial
    {
        WeldJoint __joint;

         public WeldJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB)
        {
            __joint = JointFactory.CreateWeldJoint(_world, targetA, targetB, anchorA, anchorB);
            return __joint;
        }

        public float DampingRatio
        {
            get
            {
                return __joint.DampingRatio;
            }
            set
            {
                __joint.DampingRatio = value;
            }
        }

        public float ReferenceAngle
        {
            get
            {
                return __joint.ReferenceAngle;
            }
            set
            {
                __joint.ReferenceAngle = value;
            }
        }

        public float FrequencyHz
        {
            get
            {
                return __joint.FrequencyHz;
            }
            set
            {
                __joint.FrequencyHz = value;
            }
        }

    }
}
