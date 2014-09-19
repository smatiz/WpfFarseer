using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using FarseerPhysics.Samples.SM;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarseerPhysics.Samples.Demos
{
    class CameraTests : PhysicsGameScreen, IDemoScreen
    {

        GameParameters _gameParameters = new GameParameters();

        Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }

        
        public override void LoadContent()
        {
            base.LoadContent();
            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);

          
            Body body = BodyFactory.CreateRectangle(World, 2, 2, _gameParameters.WallDensity);
            body.Position = v(0, 0);
            body.BodyType = BodyType.Dynamic;
            body.Restitution = 0.0f;
            body.Friction = 100;
            body.CollisionGroup = -1;


            Body cameraHandle = BodyFactory.CreateRectangle(World, 1, 1, _gameParameters.WallDensity);
            cameraHandle.Position = v(0, 0);
            cameraHandle.BodyType = BodyType.Dynamic;
            cameraHandle.Restitution = 0.0f;
            cameraHandle.Friction = 100;
            cameraHandle.CollisionGroup = -1;

            var joint = JointFactory.CreateDistanceJoint(World, body, cameraHandle, v(0, 0), v(0, 0));
            joint.Frequency = 1f;
            joint.DampingRatio = 10000;
           
        }
        public override void HandleInput(InputHelper input, Microsoft.Xna.Framework.GameTime gameTime)
        {

        

            base.HandleInput(input, gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public string GetTitle()
        {
            return "empty";
        }
        public string GetDetails()
        {
            return GetTitle();
        }
    }
}
