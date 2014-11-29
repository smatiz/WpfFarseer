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
        //JointInfo _jointInfo;
        string _targetNameA;
        string _targetNameB;
        protected World _world;
        protected string _id;
        protected string _anchorA;
        protected string _anchorB;

        public RopeJointMaterial(World world, JointInfo jointInfo, string id)
        {
            _world = world;
            var ropeJoint = jointInfo.Joint as IRopeJoint;
            _targetNameA = ropeJoint.TargetFlagIdA;
            _targetNameB = ropeJoint.TargetFlagIdB;
            _id = id;
        }

        private Vector2 get(BasicManager basicManager, string name)
        {
            var flag = basicManager.FindObject<IFlag>(name);
            return new Vector2(flag.X, flag.Y);
        }

        public void Finalize(BasicManager basicManager)
        {
            var fA = basicManager.FindObject<FlagInfo>(_targetNameA);
            var fB = basicManager.FindObject<FlagInfo>(_targetNameB);


            _joint = JointFactory.CreateRopeJoint(_world, basicManager.FindObject<Body>(fA.ParentId), basicManager.FindObject<Body>(fB.ParentId), fA.P.ToFarseer(), fB.P.ToFarseer());
        }

        public string TargetNameA
        {
            get
            {
                return _joint.BodyA.UserData.ToString();
            }
            set
            {
                //if (_joint != null)
                //{
                //    _joint.BodyA = _farseerWorldManager.FindObject<Body>(value);
                //}
                //else
                {
                    _targetNameA = value;
                }
            }
        }
        public string TargetNameB
        {
            get
            {
                return _joint.BodyB.UserData.ToString();
            }
            set
            {
                //if (_joint != null)
                //{
                //    _joint.BodyB = _farseerWorldManager.FindObject<Body>(value);
                //}
                //else
                {
                    _targetNameB = value;
                }
            }
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
            set
            {
                if (_joint != null)
                {
                    _joint.WorldAnchorA = value.ToFarseer();
                }
                else
                {
                    //_anchorA = value;
                }
            }
        }
        public float2 AnchorB
        {
            get
            {
                return _joint.WorldAnchorB.ToSM();
            }
            set
            {
                if (_joint != null)
                {
                    _joint.WorldAnchorB = value.ToFarseer();
                }
                else
                {
                    //_anchorB = value;
                }
            }
        }

        public object Object
        {
            get
            {
                return _joint;
            }
        }

        public string Id
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
