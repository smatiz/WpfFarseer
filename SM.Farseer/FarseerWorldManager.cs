﻿using FarseerPhysics.Collision.Shapes;
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
        World _world = new World(new Vector2(0, 10));
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
        public override void Build()
        {
            CodeGenerator.Header = Id;
            base.Build();
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

        public FarseerWorldManager(string id, IViewWatch viewWatch)
            : base(viewWatch)
        {
            Id = id;

            
            

            var worldBody = BodyFactory.CreateBody(_world);
            _fixedMouseJoint = new FixedMouseJoint(worldBody, worldBody.Position);
        }

        public string Id { get; private set; }


        

        public override IBodyMaterial CreateBodyMaterial()
        {
            return new BodyMaterial(_world);
        }
        public override IBreakableBodyMaterial CreateBreakableBodyMaterial()
        {
            return new BreakableBodyMaterial(_world);
        }



        public override IRopeJointMaterial CreateRopeJointMaterial()
        {
            return new RopeJointMaterial(_world, this);
        }
    }
}
