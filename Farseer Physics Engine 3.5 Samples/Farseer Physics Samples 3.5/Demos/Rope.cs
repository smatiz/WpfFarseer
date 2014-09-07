using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarseerPhysics.Samples.Demos
{
    class Rope
    {

        static short collisionGroup = 1;
        const int PieceCount = 35;

        private List<Body> _ropeBodies;
        List<RevoluteJoint> _ropeJoints;
        Body _chiodo;
        Body _carrucolaRightRot;

        public Rope(World world, Body hiddenBody, int wormPos)
        {


            //_chiodo = BodyFactory.CreateBody(world, Vector2.Zero);
           // _chiodo.BodyType = BodyType.Kinematic;


            var p1 = new Vector2(-10f, -10f);
            var p2 = new Vector2(-10f, 8f);
            var dl = (p2 - p1).Length() / (2f * PieceCount);// 0.5f;
            var ropeThickness = 0.1f;


            _chiodo = BodyFactory.CreateCircle(world, ropeThickness * 0.5f, 100f);
            _chiodo.Position = p1;
            _chiodo.BodyType = BodyType.Kinematic;

            Path ropePath = new Path();
            ropePath.Add(p1);
            ropePath.Add(p2);
            ropePath.Closed = false;

            Vertices box = PolygonTools.CreateRectangle(ropeThickness, dl);
            PolygonShape shape = new PolygonShape(box, PieceCount);

            _ropeBodies = PathManager.EvenlyDistributeShapesAlongPath(world, ropePath, shape, BodyType.Dynamic, PieceCount);



            /*var hWall = 1f;
            var wWall = 10;
            var _leftWall = BodyFactory.CreateRectangle(world,wWall, hWall, 1f);
            _leftWall.Position = p1 + new Vector2(-wWall * 0.5f - 2.1f*ropeThickness -1, hWall );
            _leftWall.CollisionCategories = Category.Cat1;
            _leftWall.Friction = 0;
            _leftWall.Restitution = 1;

            var _rightWall = BodyFactory.CreateRectangle(world, wWall, hWall, 1f);
            _rightWall.Position = p1 + new Vector2(wWall* 0.5f + 2.1f * ropeThickness + 1, hWall );
            _rightWall.CollisionCategories = Category.Cat1;
            _rightWall.Friction = 0;
            _rightWall.Restitution = 1;*/

            /*var _killerWall = BodyFactory.CreateRectangle(world, wWall, hWall, 1f);
            _killerWall.Position = p1 + new Vector2(0, -3 * hWall);
            _killerWall.CollisionCategories = Category.Cat1;
            _killerWall.Friction = 0;
            _killerWall.Restitution = 1;
            _killerWall.OnCollision += _killerWall_OnCollision;*/

            var hZone = 1f;
            var wZone = 10;
            var zoneUp = BodyFactory.CreateRectangle(world, 5, 1, 1f);
            zoneUp.Position = p1;// +new Vector2(-wZone * 0.5f, hZone);
            zoneUp.IsSensor = true;
            zoneUp.OnCollision += zoneUp_OnCollision;


            var zoneBottom = BodyFactory.CreateRectangle(world, 5, 1, 1f);
            zoneBottom.Position = zoneUp.Position + new Vector2(0, hZone * 2.5f);
            zoneBottom.IsSensor = true;
            zoneBottom.OnCollision += zoneBottom_OnCollision;

            var carrucolaRadius = 1;

            _carrucolaRightRot = BodyFactory.CreateCircle(world, carrucolaRadius, 1f);
            _carrucolaRightRot.Position = p1 + new Vector2(carrucolaRadius + ropeThickness * 1.1f, 0 );
            _carrucolaRightRot.CollisionCategories = Category.Cat1;
            _carrucolaRightRot.Friction = 0;
            _carrucolaRightRot.Restitution = 0;
            _carrucolaRightRot.BodyType = BodyType.Dynamic;

            
            var leftVincolo = BodyFactory.CreateCircle(world, carrucolaRadius, 1f);
            leftVincolo.Position = _carrucolaRightRot.Position + new Vector2(-2f * carrucolaRadius - ropeThickness, 2f * carrucolaRadius + 2f * ropeThickness);
            leftVincolo.CollisionCategories = Category.Cat1;
            leftVincolo.Friction = 0;
            leftVincolo.Restitution = 1;
            leftVincolo.BodyType = BodyType.Kinematic;


            var rightVincolo = BodyFactory.CreateCircle(world, carrucolaRadius, 1f);
            rightVincolo.Position = _carrucolaRightRot.Position + new Vector2(ropeThickness, 2f * carrucolaRadius + 2f * ropeThickness);
            rightVincolo.CollisionCategories = Category.Cat1;
            rightVincolo.Friction = 0;
            rightVincolo.Restitution = 1;
            rightVincolo.BodyType = BodyType.Kinematic;


            JointFactory.CreateRevoluteJoint(world, hiddenBody, _carrucolaRightRot, _carrucolaRightRot.Position, Vector2.Zero);
            //JointFactory.CreateRevoluteJoint(world, _chiodo, _carrucolaRightRot, Vector2.Zero, new Vector2(-carrucolaRadius, 0));


            //var _carrucolaLeft = BodyFactory.CreateCircle(world, carrucolaRadius, 1f);
            //_carrucolaLeft.Position = p1 + new Vector2(-carrucolaRadius - ropeThickness * 1.1f, 0);
            //_carrucolaLeft.CollisionCategories = Category.Cat1;
            //_carrucolaLeft.Friction = 0;
            //_carrucolaLeft.Restitution = 0;
            //_carrucolaLeft.BodyType = BodyType.Kinematic;

            //short _collisionGroup = collisionGroup++;
            //var cat = (Category)Math.Pow(2, _collisionGroup);
            foreach (var b in _ropeBodies)
            {
                //b.CollisionCategories = Category.None;
                b.CollidesWith = Category.Cat1;
                
                //b.IgnoreGravity = true;
                //b.Inertia = 0.001f;
                b.Friction = 0;
                b.Restitution = 1;

                //b.DestroyFixture(b.FixtureList[0]);
                //b.BodyType = BodyType.Kinematic;?
                //b.density?
               // b.Mass = 10f;
                
            }

            var worm = BodyFactory.CreateCircle(world, 0.3f, 1f);
            worm.Position = _ropeBodies[PieceCount - wormPos - 2].Position;
            worm.BodyType = BodyType.Dynamic;
            worm.CollidesWith = Category.None;

            _ropeBodies = _ropeBodies.Take(PieceCount - wormPos - 1).ToList();//.Skip(wormPos).ToList()

            JointFactory.CreateRevoluteJoint(world, worm, _ropeBodies[PieceCount - wormPos - 2], Vector2.Zero, Vector2.Zero);


            JointFactory.CreateRevoluteJoint(world, _carrucolaRightRot, _ropeBodies[0], new Vector2(-carrucolaRadius - ropeThickness, 0) ,new Vector2(0, -dl - 0.01f));
            _ropeJoints = PathManager.AttachBodiesWithRevoluteJoint(world, _ropeBodies, new Vector2(0f, -dl - 0.01f), new Vector2(0f, dl + 0.01f), false, true);





            foreach (var j in _ropeBodies.Take(5))
            {
             //   j.IsKinematic = true;
            }



            foreach (var j in _ropeJoints)
            {
                
            }

        }

        bool zoneBottom_OnCollision(Fixture fixtureA, Fixture fixtureB, Dynamics.Contacts.Contact contact)
        {
            fixtureB.CollisionGroup = 1;
            return true;
            //throw new NotImplementedException();
        }

        bool zoneUp_OnCollision(Fixture fixtureA, Fixture fixtureB, Dynamics.Contacts.Contact contact)
        {
            return true;
        }

        public void Update()
        {
            foreach (var r in _ropeBodies)
            {
               
            }
        }

        bool _killerWall_OnCollision(Fixture fixtureA, Fixture fixtureB, Dynamics.Contacts.Contact contact)
        {
            //fixtureB.Body.Enabled = false;
           // fixtureB.Body.IsKinematic = true;
            return false;
        }

        public void TiraSuCorda()
        {
            _carrucolaRightRot.ApplyAngularImpulse(100);
            //_carrucolaRightRot.AngularVelocity = 1f * (float)Math.PI ;
            //_chiodo.LinearVelocity += new Vector2(0, -1f);
        }
        public void TiraGiuCorda()
        {
            _carrucolaRightRot.ApplyAngularImpulse(-100);
            //_carrucolaRightRot.AngularVelocity = -1f * (float)Math.PI;
            //_chiodo.LinearVelocity += new Vector2(0, -1f);
        }
        public void Draw(GameTime gameTime)
        {

        }
    }
}
