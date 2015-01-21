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
    public class MouseJointManager
    {
        World _world;
        FixedMouseJoint _fixedMouseJoint = null;

        public MouseJointManager(World world)
        {
            _world = world;
        }

        public void StartMouseJoint(Body body, Vector2 position)
        {
            StopMouseJoint();
            _fixedMouseJoint = JointFactory.CreateFixedMouseJoint(_world, body, position);
        }
        public void UpdateMouseJoint(Vector2 position)
        {
            if (_fixedMouseJoint != null)
            {
                _fixedMouseJoint.WorldAnchorB = position;
            }
        }
        public void StopMouseJoint()
        {
            if (_fixedMouseJoint != null)
            {
                _world.RemoveJoint(_fixedMouseJoint);
                _fixedMouseJoint = null;
            }
        }
    }

}
