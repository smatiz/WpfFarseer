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
    public class RevoluteJointMaterial : BasicTwoPointJointMaterial, IRevoluteJointMaterial
    {
        RevoluteJoint __joint;

         public RevoluteJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB)
        {
            __joint = JointFactory.CreateRevoluteJoint(_world, targetA, targetB, anchorA, anchorB);
            return __joint;
        }

        public float JointAngle
        {
            get
            {
                return __joint.JointAngle;
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
