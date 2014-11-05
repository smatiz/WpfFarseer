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
    public abstract class BasicJointMaterial : IJointMaterial
    {
         protected World _world;
         protected FarseerWorldManager _farseerWorldManager;
         protected Joint _joint;
         protected string _id;

        public BasicJointMaterial(World world, FarseerWorldManager farseerWorldManager)
        {
            _world = world;
            _farseerWorldManager = farseerWorldManager;
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
