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
    class FixedMouseJointMaterial : BasicJointMaterial
    {
        FixedMouseJoint __joint;

        public FixedMouseJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }


        public void Build(string id, string targetNameA, float2 worldAnchor)
        {
            _id = id;
            _joint = Build(_farseerWorldManager.FindObject<Body>(targetNameA), worldAnchor.ToFarseer());
        }

        Joint Build(Body targetA, Vector2 worldAnchor)
        {
            __joint = JointFactory.CreateFixedMouseJoint(_world, targetA, worldAnchor);
            return __joint;
        }

        public float MaxForce
        {
            get
            {
                return __joint.MaxForce;
            }
            set
            {
                __joint.MaxForce = value;
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
    }
}
