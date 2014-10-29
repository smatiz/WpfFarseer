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
    public class DistanceJointMaterial : BasicTwoPointJointMaterial, IDistanceJointMaterial
    {
         DistanceJoint __joint;

         public DistanceJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB)
        {
            __joint = JointFactory.CreateDistanceJoint(_world, targetA, targetB, anchorA, anchorB);
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

        public float Frequency
        {
            get
            {
                return __joint.Frequency;
            }
            set
            {
                __joint.Frequency = value;
            }
        }

        public float Length
        {
            get
            {
                return __joint.Length;
            }
            set
            {
                __joint.Length = value;
            }
        }


    }
}
