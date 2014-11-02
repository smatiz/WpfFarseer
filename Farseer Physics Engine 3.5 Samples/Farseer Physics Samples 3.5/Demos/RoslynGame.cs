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
    public interface IExecutable
    {
        void DoIt(World World, Camera2D Camera);
    }
   
    internal class RoslynGame : PhysicsGameScreen, IDemoScreen
    {
        string _code;

        #region IDemoScreen Members

        public string GetTitle()
        {
            return "Roslyn Game";
        }

        public string GetDetails()
        {
            return GetTitle();
        }

        #endregion



        public RoslynGame(string code)
        {
            _code = code;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            //DebugView.AppendFlags(DebugViewFlags.PolygonPoints);
            //DebugView.AppendFlags(DebugViewFlags.Shape);
            Flame.Dlr.CSharpCompiler x = new Flame.Dlr.CSharpCompiler();
            //x.AddVariable(new Flame.Dlr.Variable() { Name = "World", Data = World });
            //x.AddVariable(new Flame.Dlr.Variable() { Name = "Camera", Data = Camera });
            var  r = x.Execute(_code);
            
            //_code
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