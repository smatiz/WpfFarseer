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
    class WheelJointMaterial  : BasicJointMaterial
    {
        WheelJoint __joint;

        public WheelJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        public void Build(string id, string targetNameA, string targetNameB, float2 anchor, float2 axis)
        {
            _id = id;
            __joint = Build(_farseerWorldManager.FindObject<Body>(targetNameA), _farseerWorldManager.FindObject<Body>(targetNameB), anchor.ToFarseer(), axis.ToFarseer());
        }
        protected WheelJoint Build(Body targetA, Body targetB, Vector2 anchor, Vector2 axis)
        {
            return  JointFactory.CreateWheelJoint(_world, targetA, targetB, anchor, axis);
        }


        public float2 Axis
        {
            get
            {
                return __joint.Axis.ToSM();
            }
            set
            {
                __joint.Axis = value.ToFarseer();
            }
        }
        public float2 LocalXAxis
        {
            get
            {
                return __joint.LocalXAxis.ToSM();
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
