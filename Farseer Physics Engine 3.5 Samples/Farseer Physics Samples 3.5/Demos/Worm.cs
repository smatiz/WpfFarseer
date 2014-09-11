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


    internal class Worm : PhysicsGameScreen, IDemoScreen
    {

        Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }
        const float Density = 1f;

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

            var border = new Border(World, ScreenManager, Camera);

            _hookableWalls = new HookableWalls(World, Density);

            _worm = BodyFactory.CreateCircle(World, 1, Density);
            _worm.Restitution = 0.5f;
            _worm.BodyType = BodyType.Dynamic;


            _elastic = new Elastic(World);
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
