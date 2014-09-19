using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.Demos.Prefabs;
using FarseerPhysics.Samples.ScreenSystem;
using FarseerPhysics.Samples.SM;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarseerPhysics.Samples.Demos
{

    class GameParameters
    {
        const float Size = 1f;
        const float Density = 1f;
        public float WormBodyCircleRadius { get { return Size; } }
        public float WormDensity { get { return Density; } }
        public float WallDensity { get { return 1; } }
    }

    internal class Worm : PhysicsGameScreen, IDemoScreen
    {

        GameParameters _gameParameters = new GameParameters();

        Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }

        Body _worm;
        HookableWalls _hookableWalls;
        Elastic _elastic;
        
        public override void LoadContent()
        {
            base.LoadContent();
            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);

            World.Gravity = v(0, 10);

            /*var border = new Border(World, ScreenManager, Camera);

            Body floor = BodyFactory.CreateRectangle(World, 80, 1, _gameParameters.WallDensity);
            floor.Position = v(0, 14);
            floor.Restitution = 0.0f;
            floor.Friction = 100;*/

            _hookableWalls = new HookableWalls(World, _gameParameters.WallDensity);

            _worm = BodyFactory.CreateCircle(World, _gameParameters.WormBodyCircleRadius, _gameParameters.WormDensity);
            _worm.Restitution = 0.25f;//0.9f;//0.5f;
            _worm.BodyType = BodyType.Dynamic;


            _elastic = new Elastic(World);

            Camera.Zoom = Camera.Zoom* 0.25f;
        }
        public override void HandleInput(InputHelper input, Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (input.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                var p = Camera.ConvertScreenToWorld(input.Cursor);

              var wall = _hookableWalls.GetHook(p);

                if(wall != null)
                {
                    _elastic.Launch(_worm, p, wall);
                }
                else
                {
                    _elastic.Destroy();
                }
            }

            base.HandleInput(input, gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            BasicLoop.DoUpdate();
            //Camera.Position = Camera.ConvertWorldToScreen( _worm.Position) + new Vector2(ScreenManager.GraphicsDevice.Viewport.Width*0.5f, ScreenManager.GraphicsDevice.Viewport.Height*0.5f);
            //Camera.Zoom = 
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
