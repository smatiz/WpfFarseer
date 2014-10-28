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
    class RopeJointMaterial : TwoPointJointMaterial, IRopeJointMaterial
    {
        RopeJoint __joint;

        public RopeJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB)
        {
            __joint = JointFactory.CreateRopeJoint(_world, targetA, targetB, anchorA, anchorB);
            return __joint;
        }

        public float MaxLength
        {
            get
            {
                return __joint.MaxLength;
            }
            set
            {
                __joint.MaxLength = value;
            }
        }

        public float Breakpoint
        {
            get
            {
                return __joint.Breakpoint;
            }
            set
            {
                __joint.Breakpoint = value;
            }
        }
    }
}
