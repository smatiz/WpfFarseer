using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public abstract class TwoPointJointMaterial : ITwoPointJointMaterial
    {
        protected World _world;
        FarseerWorldManager _farseerWorldManager;
        Joint _joint;
        string _id;

        public TwoPointJointMaterial(World world, FarseerWorldManager farseerWorldManager)
        {
            _world = world;
            _farseerWorldManager = farseerWorldManager;
        }


        public void Build(string id, string targetNameA, float2 anchorA, string targetNameB, float2 anchorB)
        {
            _id = id;
            _joint = Build(
                _farseerWorldManager.Find<Body>(targetNameA), anchorA.ToFarseer(),
                _farseerWorldManager.Find<Body>(targetNameB), anchorB.ToFarseer());
        }

        protected abstract Joint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB);
        public float2 AnchorA
        {
            get
            {
                return _joint.WorldAnchorA.ToFarseer();
            }
        }
        public float2 AnchorB
        {
            get
            {
                return _joint.WorldAnchorB.ToFarseer();
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