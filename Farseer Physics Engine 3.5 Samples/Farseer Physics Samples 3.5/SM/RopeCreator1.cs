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
  public  class RopeCreator1
    {
           World _world;

        public float Frequency { get; set; }
        public float DampingRatio { get; set; }
        public float Breakpoint { get; set; }

        const float Density = 1f;
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
            Body b = BodyFactory.CreateCircle(_world, Radius, 2f);
            b.FixedRotation = true;
            b.Position = position;
            b.BodyType = BodyType.Dynamic;
            return b;
        }

        public void CreateDistanceJoint(Vertices vertices, out Body[] bodies, out DistanceJoint[] distanceJoints, bool closed = false)
        {
            bodies = (from x in vertices select CreateNodeBody(x)).ToArray();
            distanceJoints = CreateDistanceJoint(bodies, closed).ToArray();
        }

        public void CreateDistanceJoint(Vertices vertices, out Body[] bodies, bool closed = false)
        {
            DistanceJoint[] distanceJoints;
            CreateDistanceJoint(vertices, out bodies, out distanceJoints, closed);
           
        }

        public DistanceJoint[] CreateDistanceJoint(Body[] bs1, Body[] bs2)
        {
            return bs1.Zip<Body, Body, DistanceJoint>(bs2, (b1, b2) => CreateDistanceJoint(b1, b2)).ToArray();
        }

        Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }

   

        DistanceJoint[] distanceJoints;
        Body[] bodyLine;

      public void Destroy()
        {
            if (bodyLine != null)
            {
                foreach (var bb in bodyLine)
                {
                    _world.RemoveBody(bb);
                }
                bodyLine = null;
            }
        }


        void LinkWithElastic(Body worm, Body wall, Vector2 wallLink, float k)
      {
          //Breakpoint = 1000;

          var n = 10;
          var xn = 1f / (float)n;


          var vl = wallLink - worm.Position;
          var dvl = xn * vl;

          var line = new Vertices(from x in Enumerable.Range(0, n + 1) select worm.Position + x * dvl);


          Destroy();
         
          CreateDistanceJoint(line, out bodyLine, out distanceJoints);

          foreach (var b in bodyLine)
          {
              b.CollisionGroup = -1;
          }


        

          JointFactory.CreateRevoluteJoint(_world, bodyLine[bodyLine.Length - 1], wall, wallLink, wallLink, true);

          //var worm = BodyFactory.CreateCircle(_world, 1, Density);
          //worm.Restitution = 0.5f;
          //worm.BodyType = BodyType.Dynamic;
          //worm.Position = bodyLine1[bodyLine1.Length - 1].Position;
          JointFactory.CreateRevoluteJoint(_world, bodyLine[0], worm, Vector2.Zero, Vector2.Zero);

          //Launch();

      }

      public void Launch()
        {
          foreach (var d in distanceJoints)
          {
              
              d.Length = d.Length / 10.0f;
          }
        }



      Body _worm;

    
        public RopeCreator1(World world)
        {
            _world = world;
            Frequency = 4.0f;
            DampingRatio =  .5f;
            Breakpoint = float.MaxValue;
            _worm = BodyFactory.CreateCircle(_world, 1, Density);
            _worm.Restitution = 0.5f;
            _worm.BodyType = BodyType.Dynamic;
        }

        public void Launch(Vector2 p, Body wall)
        {
            LinkWithElastic(_worm, wall, p, 1);
        }
    }
}
