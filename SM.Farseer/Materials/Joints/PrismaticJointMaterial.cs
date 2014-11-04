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
    class PrismaticJointMaterial : BasicAxedJointMaterial
    {
        PrismaticJoint __joint;

        public PrismaticJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Body targetB, Vector2 anchor, Vector2 axis)
        {
            __joint = JointFactory.CreatePrismaticJoint(_world, targetA, targetB, anchor, axis);
            return __joint;
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
