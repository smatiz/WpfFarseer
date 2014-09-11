using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarseerPhysics.Samples.SM
{
    class Elastic
    {
        public float Frequency { get; set; }
        public float DampingRatio { get; set; }
        public float Breakpoint { get; set; }
        public float NodeDensity { get; set; }
        public float NodeRadius { get; set; }



        DistanceJoint[] _distanceJoints;
        Body[] _nodeBodies;
        World _world;

        public Elastic(World world)
        {
            _world = world;
            Frequency = 4.0f;
            DampingRatio = .5f;
            NodeDensity = 2f;
            NodeRadius = 0.5f;
            Breakpoint = float.MaxValue;
        }

        public void Destroy()
        {
            if (_nodeBodies != null)
            {
                foreach (var bb in _nodeBodies)
                {
                    _world.RemoveBody(bb);
                }
                _nodeBodies = null;
            }
        }
        private void Launch()
        {
            foreach (var d in _distanceJoints)
            {
                d.Length = d.Length / 10.0f;
            }
        }
        public void Launch(Body launcherBody, Vector2 hookedPoint, Body hookedBody)
        {

            var n = 10;
            var xn = 1f / (float)n;


            var vl = hookedPoint - launcherBody.Position;
            var dvl = xn * vl;

            var line = new Vertices(from x in Enumerable.Range(0, n + 1) select launcherBody.Position + x * dvl);


            Destroy();

            CreateDistanceJoint(line, out _nodeBodies, out _distanceJoints);

            foreach (var b in _nodeBodies)
            {
                b.CollisionGroup = -1;
            }

            JointFactory.CreateRevoluteJoint(_world, _nodeBodies[_nodeBodies.Length - 1], hookedBody, hookedPoint, hookedPoint, true);

            JointFactory.CreateRevoluteJoint(_world, _nodeBodies[0], launcherBody, Vector2.Zero, Vector2.Zero);

            Launch();
        }

        private DistanceJoint CreateDistanceJoint(Body b1, Body b2)
        {
            DistanceJoint dj = JointFactory.CreateDistanceJoint(_world, b1, b2, Vector2.Zero, Vector2.Zero);
            dj.Frequency = Frequency;
            dj.DampingRatio = DampingRatio;
            dj.Breakpoint = Breakpoint;
            return dj;
        }
        private List<DistanceJoint> CreateDistanceJoint(Body[] bodies, bool closed = false)
        {
            var ds = new List<DistanceJoint>();
            for (int i = 1; i < bodies.Length; i++)
            {
                ds.Add(CreateDistanceJoint(bodies[i - 1], bodies[i]));
            }

            if (closed)
            {
                ds.Add(CreateDistanceJoint(bodies[bodies.Length - 1], bodies[0]));
            }

            return ds;
        }
        private Body CreateNodeBody(Vector2 position)
        {
            Body b = BodyFactory.CreateCircle(_world, NodeRadius, NodeDensity);
            b.FixedRotation = true;
            b.Position = position;
            b.BodyType = BodyType.Dynamic;
            return b;
        }
        private void CreateDistanceJoint(Vertices vertices, out Body[] bodies, out DistanceJoint[] distanceJoints, bool closed = false)
        {
            bodies = (from x in vertices select CreateNodeBody(x)).ToArray();
            distanceJoints = CreateDistanceJoint(bodies, closed).ToArray();
        }
        private void CreateDistanceJoint(Vertices vertices, out Body[] bodies, bool closed = false)
        {
            DistanceJoint[] distanceJoints;
            CreateDistanceJoint(vertices, out bodies, out distanceJoints, closed);

        }
        private DistanceJoint[] CreateDistanceJoint(Body[] bs1, Body[] bs2)
        {
            return bs1.Zip<Body, Body, DistanceJoint>(bs2, (b1, b2) => CreateDistanceJoint(b1, b2)).ToArray();
        }
        private Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }
    }
}
