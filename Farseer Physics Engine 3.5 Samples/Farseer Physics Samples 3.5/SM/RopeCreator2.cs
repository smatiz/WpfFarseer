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
    public class RopeCreator2
    {
        World _world;

        public float Frequency { get; set; }
        public float DampingRatio { get; set; }
        public float Breakpoint { get; set; }

        const float Radius = 0.5f;

        RopeJoint CreateDistanceJoint(Body b1, Body b2)
        {
            RopeJoint dj = JointFactory.CreateRopeJoint(_world, b1, b2, Vector2.Zero, Vector2.Zero);
            //dj.Frequency = Frequency;
            //dj.DampingRatio = DampingRatio;
            dj.Breakpoint = Breakpoint;
            return dj;
        }

        public List<RopeJoint> CreateDistanceJoint(Body[] bodies, bool closed = false)
        {
            var ds = new List<RopeJoint>();
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

        public Body CreateNodeBody(Vector2 position)
        {
            Body b = BodyFactory.CreateCircle(_world, Radius, 0.2f);
            b.FixedRotation = true;
            b.Position = position;
            b.BodyType = BodyType.Dynamic;
            return b;
        }

        public void CreateDistanceJoint(Vertices vertices, out Body[] bodies, out RopeJoint[] distanceJoints, bool closed = false)
        {
            bodies = (from x in vertices select CreateNodeBody(x)).ToArray();
            distanceJoints = CreateDistanceJoint(bodies, closed).ToArray();
        }

        public void CreateDistanceJoint(Vertices vertices, out Body[] bodies, bool closed = false)
        {
            RopeJoint[] distanceJoints;
            CreateDistanceJoint(vertices, out bodies, out distanceJoints, closed);

        }

        public RopeJoint[] CreateDistanceJoint(Body[] bs1, Body[] bs2)
        {
            return bs1.Zip<Body, Body, RopeJoint>(bs2, (b1, b2) => CreateDistanceJoint(b1, b2)).ToArray();
        }

        Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }

        public void Test()
        {
            var l = 3f;
            var line1 = new Vertices(new Vector2[] { v(0, 0), v(l, 0), v(2 * l, 0) });
            var line2 = new Vertices(new Vector2[] { v(0, l), v(l, l), v(2 * l, l) });
            var line3 = new Vertices(new Vector2[] { v(0, 2 * l), v(l, 2 * l), v(2 * l, 2) });

            Body[] bodyLine1, bodyLine2, bodyLine3;
            CreateDistanceJoint(line1, out bodyLine1);
            CreateDistanceJoint(line2, out bodyLine2);
            CreateDistanceJoint(line3, out bodyLine3);

            CreateDistanceJoint(bodyLine1, bodyLine2);
            CreateDistanceJoint(bodyLine2, bodyLine3);

        }

        public RopeCreator2(World world)
        {
            _world = world;
            Frequency = 4.0f;
            DampingRatio = 0.5f;
            Breakpoint = 100f;
        }
    }
}