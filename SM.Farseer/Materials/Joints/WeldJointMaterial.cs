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
    public class WeldJointMaterial : BasicJointMaterial, IDistanceJointMaterial
    {
        WeldJoint __joint;

         public WeldJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

         public void Build(string id, string targetNameA, float2 anchorA, string targetNameB, float2 anchorB)
         {
             _id = id;
             __joint = Build(
                 _farseerWorldManager.FindObject<Body>(targetNameA), anchorA.ToFarseer(),
                 _farseerWorldManager.FindObject<Body>(targetNameB), anchorB.ToFarseer());
         }
         WeldJoint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB)
        {
            return JointFactory.CreateWeldJoint(_world, targetA, targetB, anchorA, anchorB);
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
