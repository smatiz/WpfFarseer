using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Samples.Demos.Prefabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Collision.Shapes;
using System.Linq;
using FarseerPhysics.Factories;
using Flame.Dlr;
using System.Reflection;
using FarseerPhysics.Samples.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.DebugView;

public interface IExecutable
{
    void Execute(FarseerPhysics.Dynamics.World World, DebugViewXNA DebugView, FarseerPhysics.Samples.ScreenSystem.Camera2D Camera);
}

namespace FarseerPhysics.Samples.Demos
{

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

            var x = new CSharpCompiler();
            x.AddAssembly(AssemblyManager.FromAssembly(typeof(World).Assembly));
            x.AddAssembly(AssemblyManager.FromAssembly(typeof(FarseerPhysics.DebugView.DebugViewXNA).Assembly));
            x.AddAssembly(AssemblyManager.FromAssembly(typeof(IExecutable).Assembly));
            x.AddAssembly(AssemblyManager.FromAssembly(typeof(Vector2).Assembly));
            x.AddAssembly(AssemblyManager.FromAssembly(typeof(Enumerable).Assembly));
            var r = x.Execute(_code);
            var ass = r.Data as Assembly;
            if(ass == null)
            {
                return;
            }
            ((IExecutable)ass.CreateInstance("Executed")).Execute(World, DebugView   , Camera);
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