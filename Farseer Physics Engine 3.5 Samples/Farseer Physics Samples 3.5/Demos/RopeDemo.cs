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
    internal class RopeDemo : PhysicsGameScreen, IDemoScreen
    {
        private Border _border;

        private List<Body> _ropeBodies;
        List<RevoluteJoint> _ropeJoints;

        #region IDemoScreen Members

        public string GetTitle()
        {
            return "Path generator";
        }

        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            return sb.ToString();
        }

        #endregion


        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            base.HandleInput(input, gameTime);

            if (input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _rope.TiraSuCorda();
            }

            if (input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z))
            {
                _rope.TiraGiuCorda();

            }
        }


        Rope _rope;
        public override void LoadContent()
        {
            base.LoadContent();


            World.Gravity = new Vector2(0, 9.82f);

           // _border = new Border(World, ScreenManager, Camera);

            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);




          _rope=  new Rope(World, HiddenBody, 0);
           // new Rope(World, HiddenBody, 5);
           // new Rope(World, HiddenBody, 8);
           //new Rope(World, HiddenBody, 12);
        }

        public override void Draw(GameTime gameTime)
        {
            _rope.Update();



            DebugView.BeginCustomDraw(ref Camera.SimProjection, ref Camera.SimView);




            DebugView.DrawCircle(new Vector2(-10f, -10f), .1f, Color.Green);
            DebugView.DrawCircle(new Vector2(-10f, 10f), .1f, Color.Blue);

            DebugView.EndCustomDraw();
            //_border.Draw();
            base.Draw(gameTime);
        }
    }
}