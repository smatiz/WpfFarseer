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
    public class RopeJointMaterial :  IRopeJointMaterial
    {
        RopeJoint _joint;
        IRopeJoint _ropeJoint;
        protected World _world;
        protected IdInfo _id;

        protected FlagInfo _flagA;
        protected FlagInfo _flagB;

        public RopeJointMaterial(World world, RopeJointInfo jointInfo, Info info)
        {
            _world = world;
            //_ropeJoint = jointInfo.Joint as IRopeJoint;
            var targetNameA = jointInfo.FlagA.Id;
            var targetNameB = jointInfo.FlagB.Id;
        //    _flagA = info.FindFlag(targetNameA);
        //    _flagB = info.FindFlag(targetNameB);
            _id = jointInfo.Id;
        }

        private Vector2 get(BasicManager basicManager, IdInfo name)
        {
            var flag = basicManager.FindObject<IFlag>(name);
            return new Vector2(flag.X, flag.Y);
        }

        public void Finalize(Materials material)
        {
       //    _joint = JointFactory.CreateRopeJoint(_world, (Body)material.Find<BodyMaterial>(_flagA.ParentId).Object, (Body)material.Find<BodyMaterial>(_flagB.ParentId).Object, _flagA.P.ToFarseer(), _flagB.P.ToFarseer());

       //    _joint.CollideConnected = _ropeJoint.CollideConnected;
        }

        public float MaxLength
        {
            get
            {
                return _joint.MaxLength;
            }
            set
            {
                _joint.MaxLength = value;
            }
        }
        public float2 AnchorA
        {
            get
            {
                return _joint.WorldAnchorA.ToSM();
            }
        }
        public float2 AnchorB
        {
            get
            {
                return _joint.WorldAnchorB.ToSM();
            }
        }

        public object Object
        {
            get
            {
                return _joint;
            }
        }

        public IdInfo Id
        {
            get
            {
                return _id;
            }
        }

        public float Breakpoint
        {
            get
            {
                return _joint.Breakpoint;
            }
            set
            {
                _joint.Breakpoint = value;
            }
        }

        public bool CollideConnected
        {
            get
            {
                return _joint.CollideConnected;
            }
            set
            {
                _joint.CollideConnected = value;
            }
        }
    }
}
