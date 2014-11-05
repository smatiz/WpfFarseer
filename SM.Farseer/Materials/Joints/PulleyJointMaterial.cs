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
    class PulleyJointMaterial : BasicJointMaterial
    {

        PulleyJoint __joint;

        public PulleyJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }


        public void Build(string id, string targetNameA, string targetNameB, float2 anchorA, float2 anchorB, float2 worldAnchorA, float2 worldAnchorB, float ratio)
        {
            _id = id;
            _joint = Build(_farseerWorldManager.FindObject<Body>(targetNameA), _farseerWorldManager.FindObject<Body>(targetNameB), anchorA.ToFarseer(), anchorB.ToFarseer(), worldAnchorA.ToFarseer(), worldAnchorB.ToFarseer(), ratio);
        }

        Joint Build(Body targetA, Body targetB, Vector2 anchorA, Vector2 anchorB, Vector2 worldAnchorA, Vector2 worldAnchorB, float ratio)
        {
            __joint = JointFactory.CreatePulleyJoint(_world, targetA, targetB, anchorA, anchorB, worldAnchorA, worldAnchorB, ratio);
            return __joint;
        }


        public float LengthA
        {
            get
            {
                return __joint.LengthA;
            }
            set
            {
                __joint.LengthA = value;
            }
        }
        public float LengthB
        {
            get
            {
                return __joint.LengthB;
            }
            set
            {
                __joint.LengthB = value;
            }
        }
        public float CurrentLengthA
        {
            get
            {
                return __joint.CurrentLengthA;
            }
        }
        public float CurrentLengthB
        {
            get
            {
                return __joint.CurrentLengthB;
            }
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
