using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Samples.Demos.Prefabs;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Collision.Shapes;
using System.Linq;
using FarseerPhysics.Factories;

namespace FarseerPhysics.Samples.Demos
{
    internal class FromXaml : PhysicsGameScreen, IDemoScreen
    {
        private Border _border;

        #region IDemoScreen Members

        public string GetTitle()
        {
            return "Breakable bodies and explosions";
        }

        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TODO: Add sample description!");
            sb.AppendLine(string.Empty);
            sb.AppendLine("GamePad:");
            sb.AppendLine("  - Explode (at cursor): B button");
            sb.AppendLine("  - Move cursor: left thumbstick");
            sb.AppendLine("  - Grab object (beneath cursor): A button");
            sb.AppendLine("  - Drag grabbed object: left thumbstick");
            sb.AppendLine("  - Exit to menu: Back button");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Keyboard:");
            sb.AppendLine("  - Exit to menu: Escape");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Mouse / Touchscreen");
            sb.AppendLine("  - Explode (at cursor): Right click");
            sb.AppendLine("  - Grab object (beneath cursor): Left click");
            sb.AppendLine("  - Drag grabbed object: move mouse / finger");
            return sb.ToString();
        }

        #endregion



        private void GetVertices()
        {

        }


        private void test1(World W)
        {
            var vs = new Vertices() { new Vector2(0, 0), new Vector2(10, 0), new Vector2(10, 10), new Vector2(0, 10) };
            var vs2 = new Vertices() { new Vector2(15, 4), new Vector2(20, 4), new Vector2(20, 8), new Vector2(15, 8) };
             float density =  1f;



             var list = new List<FarseerPhysics.Common.Vertices>();
             list.Add(vs);
             list.Add(vs2);
         
             //var b = BodyFactory.CreateCompoundPolygon(World, list,density, BodyType.Dynamic);
             //    FixtureFactory.AttachPolygon(vs2, 1, b);
             //b.BodyType = BodyType.Dynamic;

             //FixtureFactory.AttachCircle(1, 1, b, new Vector2(-2, -2));
           




            var bb = BodyFactory.CreateBreakableBody(World, new List<Shape>() { 
                new PolygonShape(vs, 1),
                new CircleShape(1, 1),
                new PolygonShape(vs2, 1),

            },new Vector2(-10,0));
            bb.Strength = 10000;

            var b_bb = BodyFactory.CreateBody(World);


            //bb.Parts.Add(FixtureFactory.AttachCompoundPolygon(list, 1f, b_bb));


            var b2 = BodyFactory.CreateCircle(World, 1, 1, new Vector2(-10, -10));
            b2.BodyType = BodyType.Dynamic;

             var _border = new Border(World, ScreenManager, Camera);

            return;
        
            
            Camera.Zoom = 0.1f;







            var ps_1 = new List<FarseerPhysics.Collision.Shapes.PolygonShape>();
            var vs_2 = new FarseerPhysics.Common.Vertices();
            vs_2.Add(new Vector2(0, 0));
            vs_2.Add(new Vector2(100, 0));
            vs_2.Add(new Vector2(100, 100));
            var vs_2_V = new FarseerPhysics.Collision.Shapes.PolygonShape(vs_2, 1);
            ps_1.Add(vs_2_V);
            var vs_3 = new FarseerPhysics.Common.Vertices();
            vs_3.Add(new Vector2(100, 50));
            vs_3.Add(new Vector2(150, 0));
            vs_3.Add(new Vector2(150, 100));
            var vs_3_V = new FarseerPhysics.Collision.Shapes.PolygonShape(vs_3, 1);
            ps_1.Add(vs_3_V);
            var bb_4 = BodyFactory.CreateBreakableBody(W, ps_1);
            bb_4.Strength = 1;
            Body body_b = BodyFactory.CreateBody(W);
            body_b.UserData = "b";
            body_b.BodyType = BodyType.Static;
            var vs_5 = new FarseerPhysics.Common.Vertices();
            vs_5.Add(new Vector2(0, 200));
            vs_5.Add(new Vector2(300, 200));
            vs_5.Add(new Vector2(150, 250));
            FarseerPhysics.Factories.FixtureFactory.AttachPolygon(vs_5, 0, body_b);
        }

        public override void LoadContent()
        {
            //Settings.MaxPolygonVertices = 40;// (from x in list select x.Count).Max();

            base.LoadContent();

            DebugView.AppendFlags(DebugViewFlags.PolygonPoints);
            DebugView.AppendFlags(DebugViewFlags.Shape);

            World.Gravity = new Vector2(0, 10);
            test1(World);
        }

        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
           
            base.HandleInput(input, gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void UnloadContent()
        {
            DebugView.RemoveFlags(DebugViewFlags.Shape);

            base.UnloadContent();
        }
    }
}