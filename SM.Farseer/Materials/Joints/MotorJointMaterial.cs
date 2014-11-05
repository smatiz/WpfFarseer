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
    class MotorJointMaterial : BasicJointMaterial
    {

        MotorJoint __joint;

        public MotorJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }


        public void Build(string id, string targetNameA, string targetNameB)
        {
            _id = id;
            _joint = Build(_farseerWorldManager.FindObject<Body>(targetNameA), _farseerWorldManager.FindObject<Body>(targetNameB));
        }


        MotorJoint Build(Body targetA, Body targetB)
        {
            return JointFactory.CreateMotorJoint(_world, targetA, targetB);
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
        public float2 LinearOffset
        {
            get
            {
                return __joint.LinearOffset.ToSM();
            }
            set
            {
                __joint.LinearOffset = value.ToFarseer();
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
