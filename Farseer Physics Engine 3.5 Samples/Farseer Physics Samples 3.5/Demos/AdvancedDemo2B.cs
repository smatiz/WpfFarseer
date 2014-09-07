using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.Demos.Prefabs;
using FarseerPhysics.Samples.DrawingSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics.Joints;

namespace FarseerPhysics.Samples.Demos
{
    internal class AdvancedDemo2B : PhysicsGameScreen, IDemoScreen
    {
        private Border _border;

        private List<Body> _bridgeBodies;

        private Sprite _bridgeBox;
        private List<Body> _softBodies;
        private Sprite _softBodyBox;
        private Sprite _softBodyCircle;

        #region IDemoScreen Members

        public string GetTitle()
        {
            return "Path generator";
        }

        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TODO: Add sample description!");
            sb.AppendLine(string.Empty);
            sb.AppendLine("GamePad:");
            sb.AppendLine("  - Move cursor: left thumbstick");
            sb.AppendLine("  - Grab object (beneath cursor): A button");
            sb.AppendLine("  - Drag grabbed object: left thumbstick");
            sb.AppendLine("  - Exit to menu: Back button");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Keyboard:");
            sb.AppendLine("  - Exit to menu: Escape");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Mouse / Touchscreen");
            sb.AppendLine("  - Grab object (beneath cursor): Left click");
            sb.AppendLine("  - Drag grabbed object: move mouse / finger");
            return sb.ToString();
        }

        #endregion

        const int L = 1;
        const float PieceMass = 0.1f;
        const float WormMass = 10f;
        int _index = 0;
        float[] _massMod = new float[L];

        List<RevoluteJoint> _rope;

        void RefreshMass()
        {
            foreach (var bb in _bridgeBodies)
            {
                bb.Mass = PieceMass;
            }

            for (int i = 0; i < L; i++)
                _bridgeBodies[_index + i].Mass = PieceMass + _massMod[i];
        }

        WeldJoint wj;
        Body mass;
        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            base.HandleInput(input, gameTime);
            bool moved = false;
          
            if(input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                moved = true;
                _index--;
                if (_index < 0)
                    _index = 0;
            }

            var d = new Vector2(0, .1f);
            var d2 = new Vector2(.1f, 0);
            if (input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                //wj.LocalAnchorA = wj.LocalAnchorA + d;
                wj.LocalAnchorB = wj.LocalAnchorB + d2;
               // mass.Position = mass.Position + d;
               
                
                // wj.Enabled = true;

            }





            if (input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X))
            {
                //wj.LocalAnchorA = wj.LocalAnchorA - d;
                wj.LocalAnchorB = wj.LocalAnchorB - d2;
                //mass.Position = mass.Position - d;
            }

            return;

            if (input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z))
            {
                moved = true;
                _index++;
                if (_index > _bridgeBodies.Count - 1 - L)
                    _index = _bridgeBodies.Count - 1 - L;
            }

            if(moved)
            {
                RefreshMass();
            }
        }



        // pendolo con peso movibile
        void test1()
        {
            for (int i = 0; i < L; i++)
                _massMod[i] = 2 * WormMass * (L - System.Math.Abs(L * 0.5f - i)) / L;


            World.Gravity = new Vector2(0, 9.82f);

            _border = new Border(World, ScreenManager, Camera);

            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);
            Body bar = BodyFactory.CreateRectangle(World, 10, 1, 1);
            //bar.Mass = 0.1f;
            bar.BodyType = BodyType.Dynamic;

            mass = BodyFactory.CreateRectangle(World, 1, 1, 2);
            // mass.Mass = 10000;
            mass.BodyType = BodyType.Dynamic;

            wj = JointFactory.CreateWeldJoint(World, mass, bar, Vector2.Zero, new Vector2(10, 0));
            JointFactory.CreateRopeJoint(World, HiddenBody, bar, new Vector2(5, 0), new Vector2(5, 0));
        }

        // corda (mal fatta)
        void test2()
        {
            for (int i = 0; i < L; i++)
                _massMod[i] = 2 * WormMass * (L - System.Math.Abs(L * 0.5f - i)) / L;


            World.Gravity = new Vector2(0, 9.82f);

            _border = new Border(World, ScreenManager, Camera);

          
            {
                DebugView.AppendFlags(DebugViewFlags.Shape);
                DebugView.AppendFlags(DebugViewFlags.Joint);
                DebugView.AppendFlags(DebugViewFlags.CenterOfMass);

                Path bridgePath = new Path();
                bridgePath.Add(new Vector2(-10f, -10f));
                bridgePath.Add(new Vector2(-10f, 10));
                bridgePath.Closed = false;

                Vertices box = PolygonTools.CreateRectangle(0.1f, 0.5f);
                PolygonShape shape = new PolygonShape(box, 20);

                _bridgeBodies = PathManager.EvenlyDistributeShapesAlongPath(World, bridgePath, shape, BodyType.Dynamic, 20);

                //RefreshMass();

                //xna
                _bridgeBox = new Sprite(ScreenManager.Assets.TextureFromShape(shape, MaterialType.Waves, Color.Red, 1f));



                //Attach the first and last fixtures to the world
                JointFactory.CreateAngleJoint(World, HiddenBody, _bridgeBodies[0]);
                JointFactory.CreateDistanceJoint(World, HiddenBody, _bridgeBodies[0], new Vector2(-10f, -10), new Vector2(0f, -0.5f));




                for (int i = 0; i < _bridgeBodies.Count - 1; i++)
                {
                    //JointFactory.CreateRopeJoint(World, _bridgeBodies[i], _bridgeBodies[i + 1], new Vector2(0f, 0.5f), new Vector2(0f, -0.5f));
                    JointFactory.CreateAngleJoint(World, _bridgeBodies[i], _bridgeBodies[i + 1]);
                    JointFactory.CreateDistanceJoint(World, _bridgeBodies[i], _bridgeBodies[i + 1], new Vector2(0f, 0.5f), new Vector2(0f, -0.5f));
                }
            }


        }

        // corda funzionante
        void test3()
        {
            for (int i = 0; i < L; i++)
                _massMod[i] = 2 * WormMass * (L - System.Math.Abs(L * 0.5f - i)) / L;


            World.Gravity = new Vector2(0, 9.82f);

            _border = new Border(World, ScreenManager, Camera);

            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);

            Path bridgePath = new Path();
            bridgePath.Add(new Vector2(-10f, -10f));
            bridgePath.Add(new Vector2(-10f, 10));
            bridgePath.Closed = false;

            Vertices box = PolygonTools.CreateRectangle(0.1f, 0.5f);
            PolygonShape shape = new PolygonShape(box, 20);

            _bridgeBodies = PathManager.EvenlyDistributeShapesAlongPath(World, bridgePath, shape, BodyType.Dynamic, 20);

            //RefreshMass();

            //xna
            _bridgeBox = new Sprite(ScreenManager.Assets.TextureFromShape(shape, MaterialType.Waves, Color.Red, 1f));




            JointFactory.CreateRevoluteJoint(World, HiddenBody, _bridgeBodies[0], new Vector2(0, 0.5f));
            // JointFactory.CreateRevoluteJoint(World, HiddenBody, _bridgeBodies[_bridgeBodies.Count - 1], new Vector2(0, 0.5f));

            _rope = PathManager.AttachBodiesWithRevoluteJoint(World, _bridgeBodies, new Vector2(0f, -0.5f), new Vector2(0f, 0.5f), false, true);






            if (_rope != null)
            {
                foreach (var x in _rope)
                {
                    x.SetLimits(0, 0);// = JointType.FixedDistance;
                }
            }
            //_index = _bridgeBodies.Count - 1 - L;



        }


        public override void LoadContent()
        {
            base.LoadContent();

            test3();
            return;

            

            for (int i = 0; i < L; i++)
                _massMod[i] = 2 * WormMass * (L - System.Math.Abs(L * 0.5f - i)) / L;


            World.Gravity = new Vector2(0, 9.82f);

            _border = new Border(World, ScreenManager, Camera);

            /* Bridge */
            //We make a path using 2 points.
            /*{
                Path bridgePath = new Path();
                bridgePath.Add(new Vector2(-15, 5));
                bridgePath.Add(new Vector2(15, 5));
                bridgePath.Closed = false;

                Vertices box = PolygonTools.CreateRectangle(0.125f, 0.5f);
                PolygonShape shape = new PolygonShape(box, 20);

                _bridgeBodies = PathManager.EvenlyDistributeShapesAlongPath(World, bridgePath, shape, BodyType.Dynamic, 29);
                _bridgeBox = new Sprite(ScreenManager.Assets.TextureFromShape(shape, MaterialType.Waves, Color.SandyBrown, 1f));

                //Attach the first and last fixtures to the world
                JointFactory.CreateRevoluteJoint(World, HiddenBody, _bridgeBodies[0], new Vector2(0f, -0.5f));
                JointFactory.CreateRevoluteJoint(World, HiddenBody, _bridgeBodies[_bridgeBodies.Count - 1], new Vector2(0, 0.5f));

                PathManager.AttachBodiesWithRevoluteJoint(World, _bridgeBodies, new Vector2(0f, -0.5f), new Vector2(0f, 0.5f), false, true);

            }*/
            //LinkFactory.CreateChain(World, new Vector2(0f, -5f), new Vector2(3, 5), 1, 1, 10, 1, false);

            {
                DebugView.AppendFlags(DebugViewFlags.Shape);
                DebugView.AppendFlags(DebugViewFlags.Joint);
                DebugView.AppendFlags(DebugViewFlags.CenterOfMass);
               
                Path bridgePath = new Path();
                bridgePath.Add(new Vector2(-10f, -10f));
                bridgePath.Add(new Vector2(-10f, 10));
                bridgePath.Closed = false;

                Vertices box = PolygonTools.CreateRectangle(0.1f, 0.5f);
                PolygonShape shape = new PolygonShape(box, 20);

                _bridgeBodies = PathManager.EvenlyDistributeShapesAlongPath(World, bridgePath, shape, BodyType.Dynamic, 20);

                //RefreshMass();

                //xna
                _bridgeBox = new Sprite(ScreenManager.Assets.TextureFromShape(shape, MaterialType.Waves, Color.Red, 1f));

                //_bridgeBodies[0].CreateFixture(


                //Attach the first and last fixtures to the world
                //JointFactory.CreateAngleJoint(World, HiddenBody, _bridgeBodies[0]);
                //JointFactory.CreateDistanceJoint(World, HiddenBody, _bridgeBodies[0], new Vector2(-10f, -10), new Vector2(0f, -0.5f));

                JointFactory.CreateRevoluteJoint(World, HiddenBody, _bridgeBodies[0], new Vector2(0, 0.5f));
               // JointFactory.CreateRevoluteJoint(World, HiddenBody, _bridgeBodies[_bridgeBodies.Count - 1], new Vector2(0, 0.5f));

                _rope = PathManager.AttachBodiesWithRevoluteJoint(World, _bridgeBodies, new Vector2(0f, -0.5f), new Vector2(0f, 0.5f), false, true);



                for (int i = 0; i < _bridgeBodies.Count - 1; i++)
                {
                    //JointFactory.CreateRopeJoint(World, _bridgeBodies[i], _bridgeBodies[i + 1], new Vector2(0f, 0.5f), new Vector2(0f, -0.5f));
                   // JointFactory.CreateAngleJoint(World, _bridgeBodies[i], _bridgeBodies[i + 1]);
                   // JointFactory.CreateDistanceJoint(World, _bridgeBodies[i], _bridgeBodies[i + 1], new Vector2(0f, 0.5f), new Vector2(0f, -0.5f));
                }
            }

            if (_rope != null)
            {
                foreach (var x in _rope)
                {
                    x.SetLimits(0, 0);// = JointType.FixedDistance;
                }
            }
            //_index = _bridgeBodies.Count - 1 - L;




            return;



            /* Soft body */
            //We make a rectangular path.
            Path rectanglePath = new Path();
            rectanglePath.Add(new Vector2(-6, -11));
            rectanglePath.Add(new Vector2(-6, 1));
            rectanglePath.Add(new Vector2(6, 1));
            rectanglePath.Add(new Vector2(6, -11));
            rectanglePath.Closed = true;

            //Creating two shapes. A circle to form the circle and a rectangle to stabilize the soft body.
            List<Shape> shapes = new List<Shape>(2);
            shapes.Add(new PolygonShape(PolygonTools.CreateRectangle(0.5f, 0.5f, new Vector2(-0.1f, 0f), 0f), 1f));
            shapes.Add(new CircleShape(0.5f, 1f));

            //We distribute the shapes in the rectangular path.
            _softBodies = PathManager.EvenlyDistributeShapesAlongPath(World, rectanglePath, shapes,
                                                                      BodyType.Dynamic, 30);
            _softBodyBox = new Sprite(ScreenManager.Assets.TextureFromShape(shapes[0], MaterialType.Blank, Color.Silver * 0.8f, 1f));
            _softBodyBox.Origin += new Vector2(ConvertUnits.ToDisplayUnits(0.1f), 0f);
            _softBodyCircle = new Sprite(ScreenManager.Assets.TextureFromShape(shapes[1], MaterialType.Waves, Color.Silver, 1f));

            //Attach the bodies together with revolute joints. The rectangular form will converge to a circular form.
            PathManager.AttachBodiesWithRevoluteJoint(World, _softBodies, new Vector2(0f, -0.5f), new Vector2(0f, 0.5f),
                                                      true, true);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null, Camera.View);

            if (_softBodies != null)
            {
                for (int i = 0; i < _softBodies.Count; ++i)
                {
                    ScreenManager.SpriteBatch.Draw(_softBodyBox.Texture, ConvertUnits.ToDisplayUnits(_softBodies[i].Position), null, Color.White, _softBodies[i].Rotation, _softBodyBox.Origin, 1f, SpriteEffects.None, 0f);
                }

                for (int i = 0; i < _softBodies.Count; ++i)
                {
                    ScreenManager.SpriteBatch.Draw(_softBodyCircle.Texture, ConvertUnits.ToDisplayUnits(_softBodies[i].Position), null, Color.White, _softBodies[i].Rotation, _softBodyCircle.Origin, 1f, SpriteEffects.None, 0f);
                }
            }

            if (_bridgeBodies != null)
            {
                for (int i = 0; i < _bridgeBodies.Count; ++i)
                {
                    if (i != _index)
                        ScreenManager.SpriteBatch.Draw(_bridgeBox.Texture, ConvertUnits.ToDisplayUnits(_bridgeBodies[i].Position), null, Color.White, _bridgeBodies[i].Rotation, _bridgeBox.Origin, 1f, SpriteEffects.None, 0f);
                    else
                        ScreenManager.SpriteBatch.Draw(_bridgeBox.Texture, ConvertUnits.ToDisplayUnits(_bridgeBodies[i].Position), null, Color.Green, _bridgeBodies[i].Rotation, _bridgeBox.Origin, 1f, SpriteEffects.None, 0f);
                }
            }

            ScreenManager.SpriteBatch.End();
            _border.Draw();
            base.Draw(gameTime);
        }
    }
}