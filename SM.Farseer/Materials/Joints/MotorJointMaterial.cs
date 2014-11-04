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
    class MotorJointMaterial : BasicUnanchoredJointMaterial
    {

        MotorJoint __joint;

        public MotorJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Body targetB)
        {
            __joint = JointFactory.CreateMotorJoint(_world, targetA, targetB);
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

        public float MaxTorque
        {
            get
            {
                return __joint.MaxTorque;
            }
            set
            {
                __joint.MaxTorque = value;
            }
        }
        public Vector2 LinearOffset
        {
            get
            {
                return __joint.LinearOffset;
            }
            set
            {
                __joint.LinearOffset = value;
            }
        }
        public float AngularOffset
        {
            get
            {
                return __joint.AngularOffset;
            }
            set
            {
                __joint.AngularOffset = value;
            }
        }
    }
}
