using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarseerPhysics.Samples.Demos
{

    public class Actions
    {
        public Action<Microsoft.Xna.Framework.GameTime> Draw { get; set; }
        public Action<InputHelper, Microsoft.Xna.Framework.GameTime> HandleInput { get; set; }
        public Action<Microsoft.Xna.Framework.GameTime, bool, bool> Update { get; set; }

    }


    internal class Empty : PhysicsGameScreen, IDemoScreen
    {
        public string GetTitle()
        {
            return "empty";
        }

        public string GetDetails()
        {
            return GetTitle();
        }

        Actions _actions = new Actions();


        public override void LoadContent()
        {
            base.LoadContent();
            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);

            //BodyFactory.CreateCompoundPolygon(World, new List<Vertices>() {
            //    new Vertices(new Vector2[]{
            //        new Vector2(0, 0), 
            //        new Vector2(1, 0), 
            //        new Vector2(1, 1), 
            //        new Vector2(0, 1), 
            //    }),
            //    new Vertices(new Vector2[]{
            //        new Vector2(10, 10), 
            //        new Vector2(10, 20), 
            //        new Vector2(20, 20), 
            //        new Vector2(20, 10), 
            //    })
            //}, Density);



            //Flame.Debug.Register("ScreenManager", ScreenManager);
            Flame.Debug.Register("Actions", _actions);

            Flame.Debug.Register("ScreenManager", ScreenManager);
            Flame.Debug.Register("Camera", Camera);
            Flame.Debug.Register("World", World);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_actions.Draw != null) _actions.Draw(gameTime);
            base.Draw(gameTime);
        }


        public override void HandleInput(InputHelper input, Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_actions.HandleInput != null) _actions.HandleInput(input, gameTime);
            base.HandleInput(input, gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (_actions.Update != null) _actions.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
