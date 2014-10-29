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
    class WheelJointMaterial : BasicAxedJointMaterial
    {
        WheelJoint __joint;

        public WheelJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Body targetB, Vector2 anchor, Vector2 axis)
        {
            __joint = JointFactory.CreateWheelJoint(_world, targetA, targetB, anchor, axis);
            return __joint;
        }


        public Vector2 Axis
        {
            get
            {
                return __joint.Axis;
            }
            set
            {
                __joint.Axis = value;
            }
        }

        public Vector2 LocalXAxis
        {
            get
            {
                return __joint.LocalXAxis;
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

        public float MaxMotorTorque
        {
            get
            {
                return __joint.MaxMotorTorque;
            }
            set
            {
                __joint.MaxMotorTorque = value;
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
    }
}
