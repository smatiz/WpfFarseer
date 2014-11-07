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

        string _targetNameA;
        string _targetNameB;
        protected World _world;
        protected FarseerWorldManager _farseerWorldManager;
        protected string _id;
        protected float2 _anchorA;
        protected float2 _anchorB;

        public RopeJointMaterial(World world, FarseerWorldManager farseerWorldManager)
        {
            _world = world;
            _farseerWorldManager = farseerWorldManager;
        }


        public void Build(string id)
        {
            _id = id;

           _joint = JointFactory.CreateRopeJoint(_world, _farseerWorldManager.FindObject<Body>(_targetNameA), _farseerWorldManager.FindObject<Body>(_targetNameB), _anchorA.ToFarseer(), _anchorB.ToFarseer());
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
                    _anchorA = value;
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
                    _anchorB = value;
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
