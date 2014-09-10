using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.DrawingSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FarseerPhysics.Samples.Demos.Prefabs
{
    public class SpiderwebCreator
    {
        World _world;

        public float Frequency { get; set; }
        public float DampingRatio { get; set; }
        public float Breakpoint { get; set; }

        const float Radius = 0.5f;

        DistanceJoint CreateDistanceJoint(Body b1, Body b2)
        {
            DistanceJoint dj = JointFactory.CreateDistanceJoint(_world, b1, b2, Vector2.Zero, Vector2.Zero);
            dj.Frequency = Frequency;
            dj.DampingRatio = DampingRatio;
            dj.Breakpoint = Breakpoint;
           
            // dj.Length

            return dj;
        }

        public List<DistanceJoint> CreateDistanceJoint(Body[] bodies, bool closed = false)
        {
            var ds = new List<DistanceJoint>();
            for (int i = 1; i < bodies.Length; i++)
            {
                ds.Add(CreateDistanceJoint(bodies[i - 1], bodies[i]));
            }

            if(closed)
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

        public Body[] CreateDistanceJoint(Vertices vertices, bool closed = false)
        {
            var bs = (from x in vertices select CreateNodeBody(x)).ToArray();
            CreateDistanceJoint(bs, closed);
            return bs;
        }

        public DistanceJoint[] CreateDistanceJoint(Body[] bs1, Body[] bs2)
        {
            return bs1.Zip<Body, Body, DistanceJoint>(bs2, (b1, b2) => CreateDistanceJoint(b1, b2)).ToArray();
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

            var bodyLine1 = CreateDistanceJoint(line1);
            var bodyLine2 = CreateDistanceJoint(line2);
            var bodyLine3 = CreateDistanceJoint(line3);

            CreateDistanceJoint(bodyLine1, bodyLine2);
            CreateDistanceJoint(bodyLine2, bodyLine3);
        }


        public SpiderwebCreator(World world)
        {
            _world = world;
            Frequency = 4.0f;
            DampingRatio = 0.5f;
            Breakpoint = 100f;
        }
    }


    public class Spiderweb
    {
        private Sprite _goo;
        private Sprite _link;

        private float _radius;
        private float _spriteScale;
        private World _world;



        public Spiderweb(World world, Body ground, Vector2 position, float radius, int rings, int sides)
        {
            const float breakpoint = 100f;

            _world = world;
            _radius = radius;

            List<List<Body>> ringBodys = new List<List<Body>>(rings);

            for (int i = 1; i < rings; ++i)
            {
                Vertices vertices = PolygonTools.CreateCircle(i * 2.9f, sides);
                List<Body> bodies = new List<Body>(sides);

                //Create the first goo
                Body prev = BodyFactory.CreateCircle(world, radius, 0.2f, vertices[0]);
                prev.FixedRotation = true;
                prev.Position += position;
                prev.BodyType = BodyType.Dynamic;

                bodies.Add(prev);

                //Connect the first goo to the next
                for (int j = 1; j < vertices.Count; ++j)
                {
                    Body bod = BodyFactory.CreateCircle(world, radius, 0.2f, vertices[j]);
                    bod.FixedRotation = true;
                    bod.BodyType = BodyType.Dynamic;
                    bod.Position += position;

                    DistanceJoint dj = JointFactory.CreateDistanceJoint(world, prev, bod, Vector2.Zero, Vector2.Zero);
                    dj.Frequency = 4.0f;
                    dj.DampingRatio = 0.5f;
                    dj.Breakpoint = breakpoint;

                    prev = bod;
                    bodies.Add(bod);
                }

                //Connect the first and the last goo
                DistanceJoint djEnd = JointFactory.CreateDistanceJoint(world, bodies[0], bodies[bodies.Count - 1], Vector2.Zero, Vector2.Zero);
                djEnd.Frequency = 4.0f;
                djEnd.DampingRatio = 0.5f;
                djEnd.Breakpoint = breakpoint;

                ringBodys.Add(bodies);
            }

            //Create an outer ring
            Vertices lastRing = PolygonTools.CreateCircle(rings * 2.9f, sides);
            lastRing.Translate(ref position);

            List<Body> lastRingBodies = ringBodys[ringBodys.Count - 1];

            //Fix each of the body of the outer ring
            for (int j = 0; j < lastRingBodies.Count; ++j)
            {
                lastRingBodies[j].IsStatic = true;
            }

            //Interconnect the rings
            for (int i = 1; i < ringBodys.Count; i++)
            {
                List<Body> prev = ringBodys[i - 1];
                List<Body> current = ringBodys[i];

                for (int j = 0; j < prev.Count; j++)
                {
                    Body prevFixture = prev[j];
                    Body currentFixture = current[j];

                    DistanceJoint dj = JointFactory.CreateDistanceJoint(world, prevFixture, currentFixture, Vector2.Zero, Vector2.Zero);
                    dj.Frequency = 4.0f;
                    dj.DampingRatio = 0.5f;
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            _link = new Sprite(content.Load<Texture2D>("Samples/link"));
            _goo = new Sprite(content.Load<Texture2D>("Samples/goo"));

            _spriteScale = 2f * ConvertUnits.ToDisplayUnits(_radius) / _goo.Texture.Width;
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Joint j in _world.JointList)
            {
                if (j.Enabled && j.JointType != JointType.FixedMouse)
                {
                    Vector2 pos = ConvertUnits.ToDisplayUnits((j.WorldAnchorA + j.WorldAnchorB) / 2f);
                    Vector2 AtoB = j.WorldAnchorB - j.WorldAnchorA;
                    float distance = ConvertUnits.ToDisplayUnits(AtoB.Length()) + 8f * _spriteScale;
                    Vector2 scale = new Vector2(distance / _link.Texture.Width, _spriteScale);
                    Vector2 unitx = Vector2.UnitX;
                    float angle = (float)MathUtils.VectorAngle(ref unitx, ref AtoB);
                    batch.Draw(_link.Texture, pos, null, Color.White, angle, _link.Origin, scale, SpriteEffects.None, 0f);
                }
            }

            foreach (Body b in _world.BodyList)
            {
                if (b.Enabled && b.FixtureList.Count > 0 && b.FixtureList[0].Shape.ShapeType == ShapeType.Circle)
                {
                    batch.Draw(_goo.Texture, ConvertUnits.ToDisplayUnits(b.Position), null, Color.White, 0f, _goo.Origin, _spriteScale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}