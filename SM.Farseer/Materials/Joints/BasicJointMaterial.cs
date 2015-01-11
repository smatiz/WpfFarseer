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
        protected JointInfo _jointInfo;
        protected Joint _joint;
        protected World _world;
        protected IdInfo _id;
        public BasicJointMaterial(World world, JointInfo jointInfo)
        {
            _jointInfo = jointInfo;
            _world = world;
            _id = jointInfo.Id;
        }

        protected abstract Joint CreateJoint(Materials material);

        public void Finalize(Materials material)
        {
            if (_joint == null)
            {
                _joint = CreateJoint(material);
                lock (_jointInfo)
                {
                    _joint.CollideConnected = _jointInfo.CollideConnected;
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
