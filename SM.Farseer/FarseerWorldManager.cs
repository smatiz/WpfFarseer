using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class FarseerWorldManager : BasicWorldManager
    {
        World _world;
        FixedMouseJoint _fixedMouseJoint = null;

#if DEBUG
        public World World { get { return _world; } }
#endif

        public void StartMouseJoint(Body body, Vector2 position)
        {
            if (_fixedMouseJoint != null)
            {
                _world.RemoveJoint(_fixedMouseJoint);
            }
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

        protected override void Step(float dt)
        {
            _world.Step(dt);
        }

        protected override void Loop()
        {

        }

        public FarseerWorldManager(string id, Synchronizers synchronizers, IWatchView viewWatch, World world)
            : base(synchronizers, viewWatch)
        {
            Id = id;
            _world = world;
            var worldBody = BodyFactory.CreateBody(_world);
            _fixedMouseJoint = new FixedMouseJoint(worldBody, worldBody.Position);
        }

        public string Id { get; private set; }
    }
}
