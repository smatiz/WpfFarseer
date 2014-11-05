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
    public class RopeJointMaterial : BasicJointMaterial, IRopeJointMaterial
    {
        RopeJoint __joint;

        public RopeJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }


        public void Build(string id, string targetNameA, float2 anchorA, string targetNameB, float2 anchorB)
        {
            _id = id;
            __joint = Build(
                _farseerWorldManager.FindObject<Body>(targetNameA), anchorA.ToFarseer(),
                _farseerWorldManager.FindObject<Body>(targetNameB), anchorB.ToFarseer());
            _joint = __joint;
        }

        RopeJoint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB)
        {
            return JointFactory.CreateRopeJoint(_world, targetA, targetB, anchorA, anchorB);
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

    }
}
