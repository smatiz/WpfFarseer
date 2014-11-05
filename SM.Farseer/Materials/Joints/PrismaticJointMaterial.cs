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
    class PrismaticJointMaterial : BasicJointMaterial
    {
        PrismaticJoint __joint;

        public PrismaticJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        public void Build(string id, string targetNameA, string targetNameB, float2 anchor, float2 axis)
        {
            _id = id;
            __joint = Build(_farseerWorldManager.FindObject<Body>(targetNameA), _farseerWorldManager.FindObject<Body>(targetNameB), anchor.ToFarseer(), axis.ToFarseer());
        }

        protected PrismaticJoint Build(Body targetA, Body targetB, Vector2 anchor, Vector2 axis)
        {
            return JointFactory.CreatePrismaticJoint(_world, targetA, targetB, anchor, axis);
        }


        public float JointTranslation
        {
            get
            {
                return __joint.JointTranslation;
            }
        }
        public float JointSpeed
        {
            get
            {
                return __joint.JointSpeed;
            }
        }
        public bool LimitEnabled
        {
            get
            {
                return __joint.LimitEnabled;
            }
            set
            {
                __joint.LimitEnabled = value;
            }
        }
        public float LowerLimit
        {
            get
            {
                return __joint.LowerLimit;
            }
            set
            {
                __joint.LowerLimit = value;
            }
        }
        public float UpperLimit
        {
            get
            {
                return __joint.UpperLimit;
            }
            set
            {
                __joint.UpperLimit = value;
            }
        }
        public bool MotorEnabled
        {
            get
            {
                return __joint.MotorEnabled;
            }
            set
            {
                __joint.MotorEnabled = value;
            }
        }
        public float MotorSpeed
        {
            get
            {
                return __joint.MotorSpeed;
            }
            set
            {
                __joint.MotorSpeed = value;
            }
        }
        public float MaxMotorForce
        {
            get
            {
                return __joint.MaxMotorForce;
            }
            set
            {
                __joint.MaxMotorForce = value;
            }
        }
        public float MotorImpulse
        {
            get
            {
                return __joint.MotorImpulse;
            }
            set
            {
                __joint.MotorImpulse = value;
            }
        }
    }
}
